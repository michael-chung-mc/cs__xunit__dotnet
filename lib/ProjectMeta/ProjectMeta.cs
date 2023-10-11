using System.Diagnostics;
using System.Text.Json;

namespace LibProjectMeta;
public class ProjectMeta
{
    private string _fieldLogTxtPath = "../data/__.log";
    private string _fieldLogJsonPath = "../data/__.json";
    public ProjectMeta () 
    {
        // if (!File.Exists(_fieldLogTxtPath)) {
        //     File.Create(_fieldLogTxtPath).Dispose();
        // }
        // using (StreamWriter txtStream = File.AppendText(_fieldLogTxtPath))
        // {
        //     Trace.Listeners.Add(new TextWriterTraceListener(txtStream));
        //     Trace.AutoFlush = true;
        //     Trace.WriteLine($"{System.DateTime.Now} Calculator Log");
        // }
    }
    public void SetTxtLogPath(string path)
    {
        _fieldLogTxtPath = path;
    }
    public void SetJsonLogPath(string path)
    {
        _fieldLogJsonPath = path;
    }
    public void Log(string argData)
    {
        using (StreamWriter varTxtStream = File.AppendText(_fieldLogTxtPath))
        {
            varTxtStream.WriteLine(argData.ToString());
            Trace.WriteLine(argData.ToString());
        }
    }
    public void LogJson(LogData argData)
    {
        using (StreamWriter varJsonStream = File.AppendText(_fieldLogJsonPath))
        {
            varJsonStream.WriteLine(argData.ToJson());
        }
        Log(argData.ToJson());
    }
    public String getPPMFilename(bool linuxPath) {
        // std::time_t now = std::time(nullptr);
        // std::tm ltm = *std::localtime(&now);
        // std::stringstream ss;
        // ss << std::put_time(&ltm, "%Y%m%d_%H%M%S");
        // std::string time = ss.str();
        // std::string dirWindows = "./data/";
        // std::string dirLinux = ".\\data\\";
        // std::string dir = linuxPath ? dirLinux : dirWindows;
        // std::string path = dir + time + ".ppm";
        // std::cout << path << std::endl;
        String path = "../data/__";
        return path;
    }
    public double getEpsilon() {
        return 0.0001;
        // return 0.00001;
    }
    public int getPPMWidth() {
        return 720;
    }
    public int getPPMHeight() {
        return 480;
    }
    public int getPPMLineWidth() {
        return 70;
    }
    public double getPI()
    {
        return 3.141592653589793238463;
    }
}

public interface LogData
{
    public string ToJson ();
}