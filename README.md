# DupMe Front-end

## Install

### Unity Hub

1. Download the latest version of Unity Hub [Here](https://unity.com/download)
2. Install Unity Hub using Personal License
3. Select and install Unity 2021.1.7 from Unity Hub
4. Finish install and set-up for Unity and Visual Studio

### IDE

After installing Unity, the IDE can be launched by opening a file directly from Unity. Common IDEs are Visual Studio and Visual Studio Code.

### .NET

Development in C# requires .NET installed. If it isn't already on your machine, download and install .NET from [here](https://docs.microsoft.com/en-us/dotnet/core/install/).

---

## Setting-up

After following the installation procedures, now you are ready to set-up the project


1. Clone this repository into a folder, e.g. `dupme`. This will be your project directory.
```bash
$ git clone https://github.com/nantanitv/dupme-frontend
```
2. Create a Unity3D project in any other folder. Supposed you name the project `dupme-unity3d`.
3. Close Unity and Unity Hub.
4. Locate the folder `dupme-unity3d`, and copy/move the folder into the git repository folder `dupme`.
5. Re-open Unity and Unity Hub, and click on the project name `dupme-unity3d` to launch the project.
6. If you can see any of the game interfaces, then you are good to go!

---

## Working on this Project

Scripts are written entirely in C# (`.cs`), and they all can be found under `./Assets/Scripts`.

Unity3D is where we can develop the visual elements of the project, and also integrate scripts into existing objects "`GameObject`'s".

To open your chosen IDE, simply locate the **Project** tab in Unity3D and look for the **Scripts** folder under the **Assets** menu.

There, you can find several script files in `.cs` and some packages used in those scripts, i.e. **SocketIO**.

## C#

Working in C# can be somewhat reminding of Java. If you have experienced Java before, chances are you've already had a grasp of idea on how syntaxes work in C#.

Should you find yourself struggling with C#, the [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/) helps you go through the fundamentals of C# programming, and the [.NET API Documentation](https://docs.microsoft.com/en-us/dotnet/api/?view=dotnet-plat-ext-5.0) is a useful resource for APIs/packages provided by .NET, e.g. [System.Net.Http](https://docs.microsoft.com/en-us/dotnet/api/system.net.http?view=dotnet-plat-ext-5.0) for HTTP requests and responses, and [System.Threading.Tasks](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks?view=dotnet-plat-ext-5.0) for extending the use of async/await.

---

## Environment Variables

If applicable, environment variables are to be stored in the `.env` file, located at the root of the repository folder `./`, i.e. the file should be found at `./.env`. The content of the file should be formatted as follows:

```
VAR_NAME_1=value_to_be_stored_1
VAR_NAME_2=value_to_be_stored_2
```

without quotation marks (`"`) around variable names or values.
