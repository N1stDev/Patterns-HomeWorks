using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;

interface IBrowserFactory
{
    public IFirefox createFirefox();

    public IChrome createChrome();
}

class WindowsBrowserFactory : IBrowserFactory
{
    public IFirefox createFirefox()
    {
        WindowsFirefox firefox = new();
        firefox.runScript = "/C start firefox /new-window";
        firefox.closeScript = "/C taskkill /F /IM firefox.exe /T";

        return firefox;

    }

    public IChrome createChrome()
    {
        WindowsChrome chrome = new();
        chrome.runScript = "/C start chrome";
        chrome.closeScript = "/C taskkill /F /IM chrome.exe /T";

        return chrome;
    }
}

class LinuxBrowserFactory : IBrowserFactory
{
    public IFirefox createFirefox()
    {
        LinuxFirefox firefox = new();
        firefox.runScript = "XAUTHORITY=/root/.Xauthority sudo firefox --new-tab &";
        firefox.closeScript = "pkill -f firefox";

        return firefox;
    }

    public IChrome createChrome()
    {
        LinuxChrome chrome = new();
        chrome.runScript = "XAUTHORITY=/root/.Xauthority sudo google-chrome --no-sandbox &";
        chrome.closeScript = "pkill -f chrome";

        return chrome;
    }
}


interface IFirefox
{
    public void run();
    public void close();
    public void clearSessions();
}

interface IChrome
{
    public void run();
    public void close();
}


class WindowsFirefox : IFirefox
{
    public string runScript;
    public string closeScript;
    public string terminal = "CMD.exe";
    public void run()
    {
        System.Diagnostics.Process.Start(terminal, runScript);
    }

    public void close()
    {
        System.Diagnostics.Process.Start(terminal, closeScript);
    }

    public void clearSessions() { }
}

class LinuxFirefox : IFirefox
{
    public string runScript;
    public string closeScript;
    public string terminal = "/bin/bash";
    public void run()
    {
        System.Diagnostics.Process.Start(terminal, runScript);
    }

    public void close()
    {
        System.Diagnostics.Process.Start(terminal, closeScript);
    }

    public void clearSessions() { }
}

class WindowsChrome : IChrome
{
    public string runScript;
    public string closeScript;
    public string terminal = "CMD.exe";
    public void run()
    {
        System.Diagnostics.Process.Start(terminal, runScript);
    }

    public void close()
    {
        System.Diagnostics.Process.Start(terminal, closeScript);
    }
}

class LinuxChrome : IChrome
{
    public string runScript;
    public string closeScript;
    public string terminal = "/bin/bash";
    public void run()
    {
        System.Diagnostics.Process.Start(terminal, runScript);
    }

    public void close()
    {
        System.Diagnostics.Process.Start(terminal, closeScript);
    }
}


class Program
{
    public static void Main()
    {
        IBrowserFactory factory;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            factory = new WindowsBrowserFactory();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            factory = new LinuxBrowserFactory();
        }
        else
        {
            return;
        }

        IFirefox firefox = factory.createFirefox();
        IChrome chrome = factory.createChrome();

        firefox.run();

        Thread.Sleep(5000);

        firefox.close();

        Thread.Sleep(3000);

        chrome.run();

        Thread.Sleep(5000);

        chrome.close();
    }
}