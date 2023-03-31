
namespace Builder
{
    class SystemInfo
    {
        public string? logTitle;
        public string? os;
        public string? procArch;
        public string? procModel;
        public string? procLevel;
        public string? procCount;
        public string? sysDir;
        public string? userDomainName;
        public string? userName;
        public string? machineName;
        public string? clrVersion;

        public override string ToString()
        {
            string info = "";

            info += logTitle != null ? logTitle + "\n" : "";
            info += os != null ? "OS version: " + os + "\n" : "";
            info += procArch != null ? "Processor acrhitecture: " + procArch + "\n" : "";
            info += procModel != null ? "Processor model: " + procModel + "\n" : "";
            info += procCount != null ? "Number of processors: " + procCount + "\n" : "";
            info += sysDir != null ? "System directory: " + sysDir + "\n" : "";
            info += userDomainName != null ? "User domain name: " + userDomainName + "\n" : "";
            info += userName != null ? "User name: " + userName + "\n" : "";
            info += machineName != null ? "Name of machine: " + machineName + "\n" : "";
            info += clrVersion != null ? "CLR version: " + clrVersion + "\n" : "";

            return info;
        }
    }

    abstract class SystemInfoBuilder
    {

        public SystemInfo sysInfo;

        public void GenerateSystemInfo()
        {
            sysInfo = new SystemInfo();
        }

        public abstract void SetTitle();
        public abstract void SetOSVersion();
        public abstract void SetProcArch();
        public abstract void SetProcModel();
        public abstract void SetProcLevel();
        public abstract void SetProcCount();
        public abstract void SetSysDir();
        public abstract void SetUserDomainName();
        public abstract void SetUserName();
        public abstract void SetMachineName();
        public abstract void SetCLRVersion();
    }

    class SystemMonitor
    {
        public SystemInfo CollectSystemInfo(SystemInfoBuilder sib)
        {
            sib.GenerateSystemInfo();
            sib.SetTitle();
            sib.SetOSVersion();
            sib.SetProcArch();
            sib.SetProcModel();
            sib.SetProcLevel();
            sib.SetProcCount();
            sib.SetSysDir();
            sib.SetUserDomainName();
            sib.SetUserName();
            sib.SetMachineName();
            sib.SetCLRVersion();

            return sib.sysInfo;
        }
    }

    class ProcInfoBuilder : SystemInfoBuilder
    {
        public override void SetTitle()
        {
            this.sysInfo.logTitle = "---Processor information log---";
        }

        public override void SetOSVersion() { }

        public override void SetProcArch()
        {
            this.sysInfo.procArch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString();
        }

        public override void SetProcModel()
        {
            this.sysInfo.procModel = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
        }

        public override void SetProcLevel()
        {
            this.sysInfo.procLevel = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL").ToString();
        }

        public override void SetProcCount()
        {
            this.sysInfo.procCount = Environment.ProcessorCount.ToString();
        }

        public override void SetSysDir() { }

        public override void SetUserDomainName() { }

        public override void SetUserName() { }

        public override void SetMachineName() { }

        public override void SetCLRVersion() { }
    }

    class BaseInformationBuilder : SystemInfoBuilder
    {
        public override void SetTitle()
        {
            this.sysInfo.logTitle = "---General information log---";
        }

        public override void SetOSVersion()
        {
            this.sysInfo.os = Environment.OSVersion.ToString();
        }

        public override void SetProcArch()
        {
            this.sysInfo.procArch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString();
        }

        public override void SetProcModel() { }

        public override void SetProcLevel() { }

        public override void SetProcCount() { }

        public override void SetSysDir()
        {
            this.sysInfo.sysDir = Environment.SystemDirectory.ToString();
        }

        public override void SetUserDomainName() { }

        public override void SetUserName()
        {
            this.sysInfo.userName = Environment.UserName.ToString();
        }

        public override void SetMachineName()
        {
            this.sysInfo.machineName = Environment.MachineName.ToString();
        }

        public override void SetCLRVersion() { }
    }

    class FullInformationBuilder : SystemInfoBuilder
    {
        public override void SetTitle()
        {
            this.sysInfo.logTitle = "---Full information log---";
        }

        public override void SetOSVersion()
        {
            this.sysInfo.os = Environment.OSVersion.ToString();
        }

        public override void SetProcArch()
        {
            this.sysInfo.procArch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString();
        }

        public override void SetProcModel()
        {
            this.sysInfo.procModel = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
        }

        public override void SetProcLevel()
        {
            this.sysInfo.procLevel = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL").ToString();
        }

        public override void SetProcCount()
        {
            this.sysInfo.procCount = Environment.ProcessorCount.ToString();
        }

        public override void SetSysDir()
        {
            this.sysInfo.sysDir = Environment.SystemDirectory.ToString();
        }

        public override void SetUserDomainName()
        {
            this.sysInfo.userDomainName = Environment.UserDomainName.ToString();
        }

        public override void SetUserName()
        {
            this.sysInfo.userName = Environment.UserName.ToString();
        }

        public override void SetMachineName()
        {
            this.sysInfo.machineName = Environment.MachineName.ToString();
        }

        public override void SetCLRVersion()
        {
            this.sysInfo.clrVersion = Environment.Version.ToString();
        }
    }

    class SoftInformationBuilder : SystemInfoBuilder
    {
        public override void SetTitle()
        {
            this.sysInfo.logTitle = "---Software information log---";
        }

        public override void SetOSVersion()
        {
            this.sysInfo.os = Environment.OSVersion.ToString();
        }

        public override void SetProcArch() { }

        public override void SetProcModel() { }

        public override void SetProcLevel() { }

        public override void SetProcCount() { }

        public override void SetSysDir()
        {
            this.sysInfo.sysDir = Environment.SystemDirectory.ToString();
        }

        public override void SetUserDomainName()
        {
            this.sysInfo.userDomainName = Environment.UserDomainName.ToString();
        }

        public override void SetUserName()
        {
            this.sysInfo.userName = Environment.UserName.ToString();
        }

        public override void SetMachineName() { }

        public override void SetCLRVersion()
        {
            this.sysInfo.clrVersion = Environment.Version.ToString();
        }
    }

    class Program
    {
        public static void Main()
        {
            SystemMonitor sm = new();

            List<SystemInfoBuilder> builders = new List<SystemInfoBuilder> { new ProcInfoBuilder(), new BaseInformationBuilder(), new FullInformationBuilder(), new SoftInformationBuilder() };

            foreach (var builder in builders)
            {
                Console.WriteLine(sm.CollectSystemInfo(builder) + "\n\n");
            }
        }
    }
}