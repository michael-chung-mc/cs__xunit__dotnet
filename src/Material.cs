using LibColor;
using LibComparinator;
using LibTuple;
using LibLight;
using LibPattern;
using LibMatrix;
using LibForm;
namespace LibMaterial;

public class Material {
    public double _fieldAmbient;
    public double _fieldDiffuse;
    public double _fieldSpecular;
    public double _fieldShininess;
    public double _fieldReflective;
    public double _fieldTransparency;
    public double _fieldRefractiveIndex;
    public Color _fieldColor;
    public Pattern _fieldPattern;
    public Material()
    {
        _fieldAmbient = 0.1;
        _fieldDiffuse = 0.9;
        _fieldSpecular = 0.9;
        _fieldShininess = 200.0;
        _fieldReflective = 0.0;
        _fieldTransparency = 0.0;
        _fieldRefractiveIndex = 1.0;
        _fieldColor = new Color(1,1,1);
        SetPattern(new Pattern());
    }
	public Material(Material other)
    {
        _fieldAmbient = other._fieldAmbient;
        _fieldDiffuse = other._fieldDiffuse;
        _fieldSpecular = other._fieldSpecular;
        _fieldShininess = other._fieldShininess;
        _fieldReflective = other._fieldReflective;
        _fieldTransparency = other._fieldTransparency;
        _fieldRefractiveIndex = other._fieldRefractiveIndex;
        _fieldColor = other._fieldColor;
        SetPattern(other._fieldPattern);
    }    public bool CheckEqual(Material argOther)
    {
        Comparinator varComp = new Comparinator();
        return varComp.CheckFloat(this._fieldAmbient, argOther._fieldAmbient)
            && varComp.CheckFloat(this._fieldDiffuse, argOther._fieldDiffuse)
            && varComp.CheckFloat(this._fieldSpecular, argOther._fieldSpecular)
            && varComp.CheckFloat(this._fieldShininess, argOther._fieldShininess)
            && varComp.CheckFloat(this._fieldReflective, argOther._fieldReflective)
            && varComp.CheckFloat(this._fieldTransparency, argOther._fieldTransparency)
            && varComp.CheckFloat(this._fieldRefractiveIndex, argOther._fieldRefractiveIndex)
            && varComp.CheckTuple(this._fieldColor, argOther._fieldColor)
            && this._fieldPattern.CheckEqual(argOther._fieldPattern);
    }
    public Color GetColor(Form argObject, PointSource argLighting, SpaceTuple argPosition, SpaceTuple argEye, SpaceTuple argNormal, bool argInShadow)
    {
        Color varColor = _fieldPattern._fieldColors.Count() != 0 ? _fieldPattern.GetColor(argObject, argPosition) : _fieldColor;
        Color varShade = varColor * argLighting.mbrIntensity;
        Color varResAmbient = varShade * _fieldAmbient;
        if (argInShadow) return varResAmbient;
        SpaceTuple varLight = (argLighting.mbrPosition - argPosition).GetNormal();
        double varLightDotNormal = varLight.GetDotProduct(argNormal);
        Color varResDiffuse = new Color(0,0,0);
        Color varResSpecular = new Color(0,0,0);
        SpaceTuple varReflect;
        double varReflectDotEye;
        // double varFactor;
        if (varLightDotNormal >= 0.0) {
            varResDiffuse = varShade * (_fieldDiffuse * varLightDotNormal);
            varReflect = -(varLight.GetReflect(argNormal));
            varReflectDotEye = varReflect.GetDotProduct(argEye);
            if (varReflectDotEye > 0.0) {
                // varFactor = Math.Pow(varReflectDotEye, _fieldShininess);
                varResSpecular = argLighting.mbrIntensity * (_fieldSpecular * Math.Pow(varReflectDotEye, _fieldShininess));
            }
        }
        Color varRes = varResAmbient + (varResDiffuse + varResSpecular);
        return varRes;
    }
    public void SetPattern(Pattern argPattern) {
        _fieldPattern = argPattern;
    }
    public void RenderConsole() {
        Console.WriteLine("Material::renderConsole()");
        Console.WriteLine($"Material::renderConsole()::mbrAmbient({_fieldAmbient}");
        Console.WriteLine($"Material::renderConsole()::mbrDiffuse({_fieldDiffuse}");
        Console.WriteLine($"Material::renderConsole()::mbrSpecular({_fieldSpecular}");
        Console.WriteLine($"Material::renderConsole()::mbrShininess({_fieldShininess}");
        Console.WriteLine($"Material::renderConsole()::mbrReflective({_fieldReflective}");
        Console.WriteLine($"Material::renderConsole()::mbrColor( ");
        _fieldColor.RenderConsole();
        Console.WriteLine("Material::renderConsole()::mbrPattern( ");
        _fieldPattern.RenderConsole();
    }
};