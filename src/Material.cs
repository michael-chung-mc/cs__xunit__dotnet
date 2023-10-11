using LibColor;
using LibComparinator;
using LibTuple;
using LibLight;
using LibPattern;
namespace LibMaterial;

public class Material {
    public double mbrAmbient;
    public double mbrDiffuse;
    public double mbrSpecular;
    public double mbrShininess;
    public double mbrReflective;
    public double mbrTransparency;
    public double mbrRefractiveIndex;
    public Color mbrColor;
    public Pattern mbrPattern;
    public Material()
    {
        mbrAmbient = 0.1;
        mbrDiffuse = 0.9;
        mbrSpecular = 0.9;
        mbrShininess = 200.0;
        mbrReflective = 0.0;
        mbrTransparency = 0.0;
        mbrRefractiveIndex = 1.0;
        mbrColor = new Color(1,1,1);
        SetPattern(new Pattern());
    }
	public Material(Material other)
    {
        mbrAmbient = other.mbrAmbient;
        mbrDiffuse = other.mbrDiffuse;
        mbrSpecular = other.mbrSpecular;
        mbrShininess = other.mbrShininess;
        mbrReflective = other.mbrReflective;
        mbrTransparency = other.mbrTransparency;
        mbrRefractiveIndex = other.mbrRefractiveIndex;
        mbrColor = other.mbrColor;
        SetPattern(other.mbrPattern);
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
    public bool CheckEqual(Material other)
    {
        Comparinator ce = new Comparinator();
        return ce.CheckTuple(this.mbrColor, other.mbrColor) && ce.CheckFloat(this.mbrAmbient, other.mbrAmbient) && ce.CheckFloat(this.mbrDiffuse, other.mbrDiffuse) && ce.CheckFloat(this.mbrSpecular, other.mbrSpecular) && ce.CheckFloat(this.mbrShininess, other.mbrShininess);
    }
    public Color GetColor(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
    {
        // Console.WriteLine("Material::getColor()");
        Color varColor = mbrPattern._fieldColors.Count() != 0 ? mbrPattern.GetColorLocal(argPosition) : mbrColor;
        // Console.WriteLine("Material::getColor()::varColor(r:" << varColor.mbrRed << ",g:" << varColor.mbrGreen << ",b:" << varColor.mbrBlue << ")");
        Color varShade = varColor * argLighting.mbrIntensity;
        // Console.WriteLine("Material::getColor()::varShade(r:" << varShade.mbrRed << ",g:" << varShade.mbrGreen << ",b:" << varShade.mbrBlue << ")";
        // Console.WriteLine(" = varColor * argLighting.mbrIntensity(r." << argLighting.mbrIntensity.mbrRed << ",g:" << argLighting.mbrIntensity.mbrGreen << ",b:" << argLighting.mbrIntensity.mbrBlue);
        Color varResAmbient = varShade * mbrAmbient;
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
            varResDiffuse = varShade * (mbrDiffuse * varLightDotNormal);
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
                varFactor = Math.Pow(varReflectDotEye, mbrShininess);
                // Console.WriteLine("Material::getColor()::varFactor(" << varFactor;
                // Console.WriteLine(" = varReflectDotEye ^ mbrShininess(x:" << mbrShininess);
                varResSpecular = argLighting.mbrIntensity * (mbrSpecular * varFactor);
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
        mbrPattern = argPattern;
    }
    public void RenderConsole() {
        Console.WriteLine("Material::renderConsole()");
        Console.WriteLine($"Material::renderConsole()::mbrAmbient({mbrAmbient}");
        Console.WriteLine($"Material::renderConsole()::mbrDiffuse({mbrDiffuse}");
        Console.WriteLine($"Material::renderConsole()::mbrSpecular({mbrSpecular}");
        Console.WriteLine($"Material::renderConsole()::mbrShininess({mbrShininess}");
        Console.WriteLine($"Material::renderConsole()::mbrReflective({mbrReflective}");
        Console.WriteLine($"Material::renderConsole()::mbrColor( ");
        mbrColor.RenderConsole();
        Console.WriteLine("Material::renderConsole()::mbrPattern( ");
        mbrPattern.RenderConsole();
    }
};