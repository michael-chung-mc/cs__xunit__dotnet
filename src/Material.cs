using LibColor;
using LibComparinator;
using LibTuple;
using LibLight;
using LibPattern;
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
    }
// Material& Material::operator=(const Material other)
// {
// 	if (this == &other) return *this;
//     mbrAmbient = other.mbrAmbient;
//     mbrDiffuse = other.mbrDiffuse;
//     mbrSpecular = other.mbrSpecular;
//     mbrShininess = other.mbrShininess;
//     mbrReflective = other.mbrReflective;
//     mbrColor = other.mbrColor;
//     mbrTransparency = other.mbrTransparency;
//     mbrRefractiveIndex = other.mbrRefractiveIndex;
//     // setPattern(other.mbrPattern.get());
//     setPattern(std::make_unique<Pattern>(*other.mbrPattern.get()));
//     return *this;
// }
    public bool CheckEqual(Material argOther)
    {
        Comparinator varComp = new Comparinator();
        return varComp.CheckTuple(this._fieldColor, argOther._fieldColor) && varComp.CheckFloat(this._fieldAmbient, argOther._fieldAmbient) && varComp.CheckFloat(this._fieldDiffuse, argOther._fieldDiffuse) && varComp.CheckFloat(this._fieldSpecular, argOther._fieldSpecular) && varComp.CheckFloat(this._fieldShininess, argOther._fieldShininess);
    }
    public Color GetColor(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
    {
        // Console.WriteLine("Material::getColor()");
        Color varColor = _fieldPattern._fieldColors.Count() != 0 ? _fieldPattern.GetColor(argPosition) : _fieldColor;
        // Console.WriteLine("Material::getColor()::varColor(r:" << varColor.mbrRed << ",g:" << varColor.mbrGreen << ",b:" << varColor.mbrBlue << ")");
        Color varShade = varColor * argLighting.mbrIntensity;
        // Console.WriteLine("Material::getColor()::varShade(r:" << varShade.mbrRed << ",g:" << varShade.mbrGreen << ",b:" << varShade.mbrBlue << ")";
        // Console.WriteLine(" = varColor * argLighting.mbrIntensity(r." << argLighting.mbrIntensity.mbrRed << ",g:" << argLighting.mbrIntensity.mbrGreen << ",b:" << argLighting.mbrIntensity.mbrBlue);
        Color varResAmbient = varShade * _fieldAmbient;
        // Console.WriteLine("Material::getColor()::varResAmbient(r:" << varResAmbient.mbrRed << ",g:" << varResAmbient.mbrGreen << ",b:" << varResAmbient.mbrBlue << ")";
        // Console.WriteLine(" = varShade * mbrAmbient(" << mbrAmbient);
        if (argInShadow) return varResAmbient;
        Vector varLight = (argLighting.mbrPosition - argPosition).GetNormal();
        // Console.WriteLine("Material::getColor()::varLight(x:" << varLight.mbrX << ",y:" << varLight.mbrY << ",z:" << varLight.mbrZ << ",w:" << varLight.mbrW << ")";
        // Console.WriteLine(" = normalized(argLighting.mbrPosition(x:" << argLighting.mbrPosition.mbrX << ",y:" << argLighting.mbrPosition.mbrY << ",z:" << argLighting.mbrPosition.mbrZ << ")";
        // Console.WriteLine(" - argPosition(x:" << argPosition.mbrX << ",y:" << argPosition.mbrY << ",z:" << argPosition.mbrZ << ")");
        double varLightDotNormal = varLight.GetDotProduct(argNormal);
        // Console.WriteLine("Material::getColor()::varLightDotNormal(" << varLightDotNormal;
        // Console.WriteLine(" = dot(varLight,argNormal(x:" << argNormal.mbrX << ",y:" << argNormal.mbrY << ",z:" << argNormal.mbrZ << ",w:" << argNormal.mbrW << ")");
        Color varResDiffuse;
        Color varResSpecular;
        Vector varReflect;
        double varReflectDotEye;
        double varFactor;
        if (varLightDotNormal < 0.0)
        {
            varResDiffuse = new Color(0,0,0);
            varResSpecular = new Color(0,0,0);
        }
        else
        {
            varResDiffuse = varShade * (_fieldDiffuse * varLightDotNormal);
            // Console.WriteLine("Material::getColor()::varShade(r:" << varShade.mbrRed << ",g:" << varShade.mbrGreen << ",b:" << varShade.mbrBlue << ")";
            // Console.WriteLine(" = varColor * argLighting.mbrIntensity(r." << argLighting.mbrIntensity.mbrRed << ",g:" << argLighting.mbrIntensity.mbrGreen << ",b:" << argLighting.mbrIntensity.mbrBlue);
            // Console.WriteLine("* Material::getColor()::varResDiffuse = varShade * mbrDiffuse(" << mbrDiffuse << " * varLightDotNormal=(" << varLightDotNormal);
            varReflect = (-varLight).GetReflect(argNormal);
            varReflectDotEye = varReflect.GetDotProduct(argEye);
            // Console.WriteLine("Material::getColor()::varReflectDotEye(" << varReflectDotEye;
            // Console.WriteLine(" = dot(varReflect * argEye(x:" << argEye.mbrX  << ",y:" << argEye.mbrY  << ",z:" << argEye.mbrZ  << ",w:" << argEye.mbrW);
            if (varReflectDotEye <= 0.0)
            {
                varResSpecular = new Color(0,0,0);
            }
            else
            {
                varFactor = Math.Pow(varReflectDotEye, _fieldShininess);
                // Console.WriteLine("Material::getColor()::varFactor(" << varFactor;
                // Console.WriteLine(" = varReflectDotEye ^ mbrShininess(x:" << mbrShininess);
                varResSpecular = argLighting.mbrIntensity * (_fieldSpecular * varFactor);
                // Console.WriteLine("Material::getColor()::varResSpecular = argLighting.mbrIntensity * mbrSpecular" << mbrSpecular << "* varFactor");
            }
        }
        // Console.WriteLine("Material::getColor()::varReflect(x:" << varReflect.mbrX << ",y:" << varReflect.mbrY << ",z:" << varReflect.mbrZ << ")");
        // Console.WriteLine("Material::getColor()::varResDiffuse(r:" << varResDiffuse.mbrRed << ",g:" << varResDiffuse.mbrGreen << ",b:" << varResDiffuse.mbrBlue << ")");
        // Console.WriteLine("Material::getColor()::varResSpecular(r:" << varResSpecular.mbrRed << ",g:" << varResSpecular.mbrGreen << ",b:" << varResSpecular.mbrBlue << ")");
        Color varRes = varResAmbient + (varResDiffuse + varResSpecular);
        // return argInShadow ? varResAmbient : varResAmbient + varResDiffuse + varResSpecular;
        // Console.WriteLine("Material::getColor()->varRes(r:" << varRes.mbrRed << ",g:" << varRes.mbrGreen << ",b:" << varRes.mbrBlue << ") = varResAmbient?varResAmbient+varResDiffuse+varResSpecular");
        // Console.WriteLine(" == varResAmbient(r:" << varResAmbient.mbrRed << ",g:" << varResAmbient.mbrGreen << ",b:" << varResAmbient.mbrBlue << ")";
        // Console.WriteLine(" + varResDiffuse(r:" << varResDiffuse.mbrRed << ",g:" << varResDiffuse.mbrGreen << ",b:" << varResDiffuse.mbrBlue << ")";
        // Console.WriteLine(" + varResSpecular(r:" << varResSpecular.mbrRed << ",g:" << varResSpecular.mbrGreen << ",b:" << varResSpecular.mbrBlue << ")");
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