#include "pch.h"

Material::Material ()
{
    color = Color(1,1,1);
    ambient = 0.1;
    diffuse = 0.9;
    specular = 0.9;
    shininess = 200.0;
}

bool Material::checkEqual(Material other)
{
    Comparinator ce = Comparinator();
    return ce.checkTuple(color, other.color) && ce.checkFloat(ambient, other.ambient) && ce.checkFloat(diffuse, other.diffuse) && ce.checkFloat(specular, other.specular) && ce.checkFloat(shininess, other.shininess);
}

Color Material::getLighting(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal)
{
    Color varShade = color * argLighting.intensity;
    Vector varLight = (argLighting.position - argPosition).normalize();
    Color varResAmbient = varShade * ambient;
    double varLightDotNormal = varLight.dot(argNormal);
    Color varResDiffuse;
    Color varResSpecular;
    Vector varReflect;
    double varReflectDotEye;
    if (varLightDotNormal < 0)
    {
        varResDiffuse = Color(0,0,0);
        varResSpecular = Color(0,0,0);
    }
    else
    {
        varResDiffuse = varShade * diffuse * varLightDotNormal;
        varReflect = argNormal.reflect(-varLight);
        varReflectDotEye = varReflect.dot(argEye);
        if (varReflectDotEye <= 0)
        {
            varResSpecular = Color(0,0,0);
        }
        else
        {
            double varFactor = pow(varReflectDotEye, shininess);
            varResSpecular = argLighting.intensity * specular * varFactor;
        }
    }
    return varResAmbient + varResDiffuse + varResSpecular;
}