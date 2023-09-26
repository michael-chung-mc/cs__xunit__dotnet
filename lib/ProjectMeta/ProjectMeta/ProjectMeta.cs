using System.Diagnostics;
using System.Text.Json;

namespace LibProjectMeta;
public class ProjectMeta
{
    private string _fieldLogTxtPath = "../data/__.log";
    private string _fieldLogJsonPath = "../data/__.json";
    public ProjectMeta () 
    {
        if (!File.Exists(_fieldLogTxtPath)) {
            File.Create(_fieldLogTxtPath).Dispose();
        }
        using (StreamWriter txtStream = File.AppendText(_fieldLogTxtPath))
        {
            Trace.Listeners.Add(new TextWriterTraceListener(txtStream));
            Trace.AutoFlush = true;
            Trace.WriteLine($"{System.DateTime.Now} Calculator Log");
        }
    }
    public void SetTxtLogPath(string path)
    {
        _fieldLogTxtPath = path;
    }
    public void SetJsonLogPath(string path)
    {
        _fieldLogJsonPath = path;
    }
    public void Log(string data)
    {
        using (StreamWriter txtStream = File.AppendText(_fieldLogTxtPath))
        {
            txtStream.WriteLine(data.ToString());
            Trace.WriteLine(data.ToString());
        }
    }
    public void LogJson(LogData data)
    {
        using (StreamWriter jsonStream = File.AppendText(_fieldLogJsonPath))
        {
            jsonStream.WriteLine(data.ToJson());
        }
        Log(data.ToJson());
    }
}

public interface LogData
{
    public string ToJson ();
}