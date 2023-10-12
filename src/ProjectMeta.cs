using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;

namespace LibProjectMeta;
public class ProjectMeta
{
    public String GetDir() {
        var varSpr = Path.DirectorySeparatorChar;
        return $"..{varSpr}data{varSpr}";
    }
    public String getPPMFilename(String argName = "") {
        String time = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        return $"{GetDir()}{time}__{argName}.ppm";
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

public class ProjectLog : ProjectMeta {
    private string _fieldLogTxtPath;
    private string _fieldLogJsonPath;
    public ProjectLog () 
    {
        _fieldLogTxtPath = $"{GetDir()}__.log";
        _fieldLogJsonPath = $"{GetDir()}__.json";
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
}