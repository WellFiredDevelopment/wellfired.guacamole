using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WellFired.Guacamole.Databinding
{
    public class BindingContext : IBindingElement
    {
		private static Assembly[] assemblies;
		private static Type[] modelTypes;
		private static string[] namespaces;

        public static Assembly[] Assemblies
        {
            get
            {
                RefreshAssembly();
                return assemblies;
            }
        }

        public static Type[] ModelTypes
        {
            get
            {
                RefreshAssembly();
                return modelTypes;
            }
        }

        public static string[] NameSpaces
        {
            get
            {
                RefreshAssembly();
                return namespaces;
            }
        }

        private static void RefreshAssembly()
        {
            if(assemblies == null)
            {
                assemblies =
                    AppDomain.CurrentDomain.GetAssemblies()
                        .OrderBy(o => o.FullName)
                        .ToArray();
                modelTypes = assemblies.SelectMany(o => o.GetTypes()).Where(o => o.IsPublic).OrderBy(o => o.Name).ToArray();
                namespaces = modelTypes.Select(o => o.Namespace).OrderBy(o => o).Distinct().ToArray();
            }
        }

		public bool ModelIsMock;
		public string ModelAssembly;
		public string ModelNamespace;
		public string ModelFullName;
		public string ModelType;
		private Type _dataType;
		private object _dataInstance;
		protected bool IsWrappedBinder;

        public Type GetDataType()
        {
            return ModelTypes.FirstOrDefault(o => o.FullName == ModelFullName);
        }

        public bool HasDataType()
        {
            return !string.IsNullOrEmpty(ModelFullName) && !string.IsNullOrEmpty(ModelNamespace);
        }

        public Type DataType
        {
            get { return _dataType; }
            set
            {
                if(_dataType == value)
                    return;

                _dataType = value;

                if(value != null)
                {
                    ModelType = value.Name;
                    ModelFullName = value.FullName;
                    ModelNamespace = value.Namespace;
                    ModelAssembly = value.Assembly.FullName;
                }
            }
        }

        public object DataInstance
        {
            get { return _dataInstance; }
            set
            {
                if(_dataInstance == value)
                    return;

                OnRemoveInstance();

                _dataInstance = value;

                if(DataInstance != null)
                    OnAddInstance();
            }
        }

		protected IObservableModel BindableContext { get; set; }
        protected List<IBindingElement> Binders = new List<IBindingElement>();

        private void OnRemoveInstance()
        {
            if(BindableContext != null)
            {
                BindableContext.OnBindingUpdate -= RelayBindingUpdate;
                if(IsWrappedBinder)
                    ((ModelBinder)BindableContext).Dispose();
            }

            // remove type casts
            BindableContext = null;
            IsWrappedBinder = false;

            // set Binder DataInstance
            var array = Binders.ToArray();

            for(var i = 0; i < array.Length; i++)
            {
                //was removed
                if (array[i].Context != this)
                    continue;

                array[i].Model = null;
            }
        }

        private void OnAddInstance()
        {
            DataType = DataInstance.GetType();

            BindableContext = DataInstance as IObservableModel;

            if(BindableContext == null)
            {
                BindableContext = new ModelBinder(DataInstance);
                IsWrappedBinder = true;
            }

            BindableContext.OnBindingUpdate += RelayBindingUpdate;

            var array = Binders.ToArray();
            for(var i = 0; i < array.Length; i++)
            {
                if(array[i].Context != this)
                    continue;

                array[i].Model = BindableContext;
            }
        }

        public void SubscribeBinder(IBindingElement child)
        {
            if(!Binders.Contains(child))
                Binders.Add(child);

            child.Model = BindableContext;
        }

        public void UnsubscribeBinder(IBindingElement child)
        {
            child.Model = null;
            Binders.Remove(child);
        }

        public void ClearBinders()
        {
            Binders.Clear();
            SubscribeBinder(this);
        }

        private void RelayBindingUpdate(ObservableMessage message)
        {
            var array = Binders.ToArray();

            for(var i = 0; i < array.Length; i++)
                array[i].OnBindingMessage(message);

            OnBindingMessage(message);
        }

        private BindingContext _parentContext;
		private IObservableModel _model;

        public BindingContext Context
        {
            get { return _parentContext; }
            set
            {
                if(_parentContext == value)
                    return;

                if(_parentContext != null)
                    _parentContext.UnsubscribeBinder(this);

                _parentContext = value;

                if(_parentContext != null)
                    _parentContext.SubscribeBinder(this);
            }
        }

        public IObservableModel Model
        {
            get { return _model; }
            set
            {
                if(_model == value)
                    return;

                _model = value;

                InitialValue();
            }
        }

        private string _propertyName;

        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                if(_propertyName == value)
                    return;

                _propertyName = value;
                SetPropertyTypeData();
                InitialValue();
            }
        }

        public void OnBindingMessage(ObservableMessage message)
        {
            if(message.name == PropertyName)
            {
                DataInstance = message.value;

                var array = Binders.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    array[i].OnBindingRefresh();
                }
            }
        }

        public void OnBindingRefresh()
        {
        }

        private void InitialValue()
        {
            if(string.IsNullOrEmpty(PropertyName))
                return;
			
            if(Model == null)
                return;
			
            DataInstance = Model.GetValue(PropertyName);
        }

        private void SetPropertyTypeData()
        {
            if(string.IsNullOrEmpty(PropertyName))
                return;

            if(Context == null)
                return;

            if(Context.DataType == null)
                return;

            var member = Context.DataType.GetMember(PropertyName).FirstOrDefault();

            if(member == null)
                return;

            if(member is FieldInfo)
                DataType = ((FieldInfo) member).FieldType;

            if(member is PropertyInfo)
                DataType = ((PropertyInfo) member).PropertyType;
        }
	}
}