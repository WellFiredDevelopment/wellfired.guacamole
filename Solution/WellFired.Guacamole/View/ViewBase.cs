﻿using System;
					try
					{
						newNativeRenderer = NativeRendererHelper.CreateNativeRendererFor(GetType());
				}
				catch (NativeRendererCannotBeFound)
				{
					throw;
				}
				catch (Exception e)