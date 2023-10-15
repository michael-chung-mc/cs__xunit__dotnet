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
    public double GetEpsilon() {
        // return 0.0001;
        // return 0.00001;
        return 0.000001;
    }
    public int GetPPMWidth() {
        return 720;
    }
    public int GetPPMHeight() {
        return 480;
    }
    public int GetPPMLineWidth() {
        return 70;
    }
    public double GetPI()
    {
        return 3.141592653589793238463;
    }
}