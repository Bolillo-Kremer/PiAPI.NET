# PiAPI .NET Library
##### [Bolillo Kremer](https://youtube.com/BolilloKremer?https://www.youtube.com/BolilloKremer?sub_confirmation=1)

## Overview
This user friendly library allows you to easily interface with multiple raspberry pi's at once using [PiAPI](https://github.com/Bolillo-Kremer/PiAPI). The simplicity of this library makes it easy for anybody to use. Most functionality is based off of [onoff](https://www.npmjs.com/package/onoff), which is running on [PiAPI](https://github.com/Bolillo-Kremer/PiAPI).

For updates on this project and other other entertainging coding projects, please subscribe to my YouTube channel, [Bolillo Kremer](https://youtube.com/BolilloKremer?https://www.youtube.com/BolilloKremer?sub_confirmation=1). 

## How to use

### Requirements
This library requires that [PiAPI](https://github.com/Bolillo-Kremer/PiAPI) is running on your raspberry pi. You can intall it on your pi with only one command! For instructions, [click here](https://github.com/Bolillo-Kremer/PiAPI/blob/master/README.md).

### Initializing
You can either install the PiAPI nuget package or download PiAPI.dll by clicking [here](https://github.com/Bolillo-Kremer/PiAPI.NET/blob/master/PiAPI-latest.dll?raw=true).
After downloading the nuget package, or adding a reference to this library in your project, you will need to setup you PiAPI connection like this.

```csharp
using PiAPI;
```
```csharp
static public void Main(String[] args) 
{ 
    string IpAddress = "192.168.1.100";
    long Port = Pi.DefaultPort; //(Default port = 5000)
    
    //Initialize Pi object with IPAddress and port of pi
    Pi MyPi = new Pi(IpAddress, Port);

    //You need to specify which pins will be set as input or output
    MyPi.InitPin(2, "in");
    MyPi.InitPin(3, "out");
}
```

If you would rather provide a specific url than using an IP address and a port, you can do so like this.
```csharp
Pi MyPi = new Pi("http://192.168.1.100:5000");
```


### Interfacing

You can get the state (0 or 1) of a given pin using this function
```csharp
//Returns the state of pin 2 as a string
MyPi.GetState(2);
```

You can also get a JSON formatted string of all the pin states using this function.

```csharp
//Returns a Newtonsoft.Json.Linq.JObject
MyPi.GetStates();
```
If you want to set the state (0 or 1) of a pin, use this function
```csharp
//Sets pin 2 to state 0
MyPi.SetState(2, 0);
```
Alternatively, you can use "toggle" to toggle the pins state
```csharp
//Sets pin 2 to state 0
MyPi.SetState(2, "toggle");
```

If you wish to set the state of all initiated pins, you can do so with the SetAllStates() function.

#### Customize PiAPI

If you add any GET or POST methods to PiAPI on your Pi, you can access them with the Get and Post functions in PiAPI.Utilities.
Additionaliy, you can access the raw url of PiAPI by calling Utilities.RawUrl.

##### Example
```cshrap
//Posts "Some Content" to PiAPI
string POSTResponse = Utilities.Post(MyPi.RawUrl + "/SomePost", "Some content").Result;

//Gets Response from PiAPI
string GETResponse = Utilities.Get(MyPi.RawUrl + "/SomeGet").Result;
```

### API Settings

This library also allwos you to interface with the PiAPI settings in the PiAPISettings class.

Current Settings:
* Port (Gets or sets the port that the API is running on)
* (IN DEVELOPMENT) Keys (Gets or sets keys that must be provided upon each request)

The settings will take place on server reboot.

#### Example
```csharp
//Changes port to 5000
MyPi.SetAPIPort(5000);
```

### Extensions
Responses from the server will always come return in the form of a string. The following extensions can parse JSON formatted strings into other objects.
* ToDictionary() (Converts a valid string to a dictionary<string, string>)
* ToArray() (Converts a valid string to a string[])
* ToJObject() (Converts a valid string to a [Newtonsoft JObject](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Linq_JObject.htm))

Objects can also be converted back into a JSON formatted string with this extension
* ToJSON() (Converts a valid object to a JSON formatted string)

### Other Functions

Precise timing can be hard to accomplish when dealing with web servers. Your internet connection can affect the speed at which certain tasks execute, so timing can be an issue.

The Delay function allows you to delay the next line in the thread while executing the current line. This will allow you to more precisely time out events. You can achieve this by doing the following
```csharp
//Delays the next line in the thread while executing the current line
//Writes the state of pin 2 to the console and delays the next line by 500ms
Utilities.Delay(() => Console.WriteLine(Pi.GetState(2)), 500);
```
Alternatively, you can use the delay function like this
```csharp
//Sets action
public static Action GetPin2() 
{
    return () => Console.WriteLine(Pi.GetState(2));
}

static public void Main(String[] args)
{
    //Initialize Pi object
    Pi MyPi = new Pi("192.168.1.100", 5000)

    //OPTIONAL
    //All pins will be set to out as default
    //You need to specify which pins will be set as input
    MyPi.InitPins.add(2, "in");

    //Delays the next line by 500ms after the current line is called
    GetPin2().Delay(500);
    Console.WriteLine("It has been 500ms");
}

```
Note: If the current line takes longer to execute than the given delay time, then the next line will execute once the current line is done.

For updates on this project and other other entertainging coding projects, please subscribe to my YouTube channel, [Bolillo Kremer](https://youtube.com/BolilloKremer?https://www.youtube.com/BolilloKremer?sub_confirmation=1). 
