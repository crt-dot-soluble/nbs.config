﻿using Nbs.Config;

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

// proprties can be created by instantiating a new Property object.
var customPropery = new Property("customName", "CustomValue");

// You can add properties to the config by calling config.Add(<property>)
config.Add(customPropery);

// You can save configs by calling NbsManager.Save(<filePath>, <config>).
// If the save succeeds the function will return true.
// Otherwise it will return false.
// NOTE: The filename and extension of the config file does not matter.
//       If you intend to overwrite the existing config, you must save it to the same location.
var success = NbsManager.Save(config, targetFilePath);

// You can also output the result of the save to the console.
//Console.WriteLine($"Sucess: {NbsManager.Save(config, targetFilePath)}");

// Check to see that the config has been updated.
Console.WriteLine(config);

// You can also remove properties from the config by calling config.Remove(<property>)
config.Remove("customName");

// Save again to finalize changes
NbsManager.Save(config, targetFilePath);

// Check to see the item has been removed
Console.WriteLine(config);
