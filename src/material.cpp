#include "material.h"
#include "tuple.h"
#include "light.h"
#include "color.h"
#include "comparinator.h"
#include "pattern.h"
#include <cmath>

Material::Material ()
{
    mbrAmbient = 0.1;
    mbrDiffuse = 0.9;
    mbrSpecular = 0.9;
    mbrShininess = 200.0;
    mbrColor = Color(1,1,1);
    mbrPattern = new Pattern();
}

bool Material::checkEqual(Material other)
{
    Comparinator ce = Comparinator();
    return ce.checkTuple(mbrColor, other.mbrColor) && ce.checkFloat(mbrAmbient, other.mbrAmbient) && ce.checkFloat(mbrDiffuse, other.mbrDiffuse) && ce.checkFloat(mbrSpecular, other.mbrSpecular) && ce.checkFloat(mbrShininess, other.mbrShininess);
}

Color Material::getColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
{
    Color varColor = mbrPattern->mbrColors.size() != 0 ? mbrPattern->getColor(argPosition) : mbrColor;
    Color varShade = varColor * argLighting.mbrIntensity;
    Vector varLight = (argLighting.mbrPosition - argPosition).normalize();
    Color varResAmbient = varShade * mbrAmbient;
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
        varResDiffuse = varShade * mbrDiffuse * varLightDotNormal;
        varReflect = (-varLight).reflect(argNormal);
        varReflectDotEye = varReflect.dot(argEye);
        if (varReflectDotEye <= 0)
        {
            varResSpecular = Color(0,0,0);
        }
        else
        {
            double varFactor = pow(varReflectDotEye, mbrShininess);
            varResSpecular = argLighting.mbrIntensity * mbrSpecular * varFactor;
        }
    }
    return argInShadow ? varResAmbient : varResAmbient + varResDiffuse + varResSpecular;
}
void Material::setPattern(Pattern *argPattern) {
    mbrPattern = argPattern;
}