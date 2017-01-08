### Homebrew

"Homebrew installs the stuff you need that Apple didn't"
1)  ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"


### Mono [Optional]
Unity's mono is old, even the new version, lets keep our version up to date.

1)  brew update
2)  brew install mono


### Node.js
Node is a JavaScript runtime upon which many of our automation scripts are built.

1)  brew update
2)  brew install node


### CoffeeScript
"CoffeeScript is a little language that compiles into JavaScript. Underneath that awkward Java-esque patina, JavaScript 
 has always had a gorgeous heart. CoffeeScript is an attempt to expose the good parts of JavaScript  in a simple way."

1)  npm install --global coffee-script


### Git-LFS

1) brew install git-lfs


### Node Package Manager

1) cd project root
2) npm install


### Task Runner
Use Jake to automate all the things. Jake is built on Node.js so we can use Node's package manager.

1)  Install jake
     npm install --global jake

    If required, take ownership of your npm directory:
     sudo chown -R $(whoami) ~/.npm

    Once installed, jake can be invoked from anywhere within the Project on the command line

2)  jake -T    to see what tasks are available

### Visual Studio Code
To allow friendly editing of Unity csharp files and debugging, install Visual Studio Code.

1)  Install Visual Studio Code
     https://code.visualstudio.com/Download