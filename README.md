
# nbs.config

A simple solution for simple configurations.

nbs.config is a simple config mananger that is based on key-value pairs. There are no objects within nbs.config in the sense they exist in other similar languages (TOML, XML, JSON) instead, nbs.config deals exclusively in a dictionary of key-value pairs where both the key and value are String objects. nbs.config provides the utilies to save, load, and interpret these config files.

Implementation is left to the developer. No types are inferred, all keypair values are returned as native strings.

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)

There are no additional dependencies or included packages.

## Compile From  Source

- Clone the repo:
  
  `git clone https://github.com/crt-dot-soluble/nbs.config.git`

- Navigate to the cloned directory

  `cd nbs.config`

- Compile the assembly from within the root directory (.../nbs.config/):

  `dotnet build`

## Add to an Existing Project

You must reference the compiled nbs.config assembly (nbs.config.dll) from your .csproj file. This can by modifying your .csproj file to include the following lines:

```xml
<ItemGroup>

...

  <Reference Include="nbs.config">
    <HintPath>".\lib\nbs.config.dll</HintPath>
  </Reference>

...

</ItemGroup>
```

In the above scenario I am assuming you have copied the compiled binary to a folder called 'lib' in the root of your project.

## Syntax

### Key-Value Pairs

`[name]=[value]`

Where `[name]` and `[value]` are converted to a Dictionary (key-value pair), and the `=` serves as a delimeter between the two.

### Comments

`#This is a comment`

Comments are entirely ingored during parsing and act the same as whitespace.

### Whitespace

Entire lines of whitespace are entirely ignored.
Trailing and leading whitespace is also ignored.

The following valid nbs.config file shows several valid entries with different consistency:

```toml
#First valid comment
# Second valid comment

firstProperty=firstValue
   secondProperty = secondProperty
thirdProperty    =    thirdProperty
  
```

When parsed and re-saved the intended format is recreated:

```toml
#First valid comment
#Second valid comment

firstProperty=firstValue
secondProperty=secondProperty
thirdProperty=thirdProperty
  
```

## Example

The raw sourcecode is available in /example/

```cs
using Nbs.Config;

// Because we are reading and writing to the same file we will define a variable to store it.
var targetFilePath = "nbs.config";

// You can silence debug info by setting Output to false
NbsManager.Output = false;

// You can create an empty config by instantiating a new Config object.
// var blankConfig = new Config();

// Configs are loaded by calling NbsManager.Load(<filePath>).
// NOTE: The filename and extension of the config file does not matter.
//       The contents of the file is all the Parser is concerned with.
var config = NbsManager.Load(targetFilePath);

// Assuming no errors are indicated, test to make sure the config is loaded.
Console.WriteLine(config);

// Properties can be created by instantiating a new Property object.
var customPropery = new Property("customName", "CustomValue");

// You can add properties to the config by calling config.Add(<property>)
config.Add(customPropery);

// You can save configs by calling NbsManager.Save(<filePath>, <config>).
// If the save succeeds the function will return true.
// Otherwise it will return false.
// NOTE: The filename and extension of the config file does not matter.
//       If you intend to overwrite the existing config, you must save it to the same location.
Console.WriteLine($"Sucess: {NbsManager.Save(config, targetFilePath)}");

// Check to see that the config has been updated.
Console.WriteLine(config);

// You can also remove properties from the config by calling config.Remove(<property>)
config.Remove("customName");

// Check to see the item has been removed
Console.WriteLine(config);
```

Should yield the following output: **(NbsManager.Output = false)**

```toml
nbs.config-------------------------------------
author=crt.soluble
library=nbs.config
-----------------------------------------------
nbs.config-------------------------------------
author=crt.soluble
library=nbs.config
customName=CustomValue 
-----------------------------------------------
nbs.config-------------------------------------
author=crt.soluble
library=nbs.config
-----------------------------------------------
```
