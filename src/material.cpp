#include "material.h"
#include "tuple.h"
#include "light.h"
#include "color.h"
#include "comparinator.h"
#include "pattern.h"
#include <memory>
#include <cmath>

Material::Material ()
{
    mbrAmbient = 0.1;
    mbrDiffuse = 0.9;
    mbrSpecular = 0.9;
    mbrShininess = 200.0;
    mbrColor = Color(1,1,1);
    mbrPattern = std::make_unique<Pattern>(Pattern());
}
Material::Material(const Material& other)
{
    mbrAmbient = other.mbrAmbient;
    mbrDiffuse = other.mbrDiffuse;
    mbrSpecular = other.mbrSpecular;
    mbrShininess = other.mbrShininess;
    mbrColor = other.mbrColor;
    setPattern(other.mbrPattern.get());
}
Material::~Material() {
}
Material& Material::operator=(const Material other)
{
	if (this == &other) return *this;
    mbrAmbient = other.mbrAmbient;
    mbrDiffuse = other.mbrDiffuse;
    mbrSpecular = other.mbrSpecular;
    mbrShininess = other.mbrShininess;
    mbrColor = other.mbrColor;
    setPattern(other.mbrPattern.get());
    return *this;
}
bool Material::checkEqual(Material other)
{
    Comparinator ce = Comparinator();
    return ce.checkTuple(mbrColor, other.mbrColor) && ce.checkFloat(mbrAmbient, other.mbrAmbient) && ce.checkFloat(mbrDiffuse, other.mbrDiffuse) && ce.checkFloat(mbrSpecular, other.mbrSpecular) && ce.checkFloat(mbrShininess, other.mbrShininess);
}

Color Material::getColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
{
    Color varColor = mbrPattern->mbrColors.size() != 0 ? mbrPattern->getColorLocal(argPosition) : mbrColor;
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
    if (PatternStripe *varPS = dynamic_cast<PatternStripe *>(argPattern))
    {
        mbrPattern = std::make_unique<PatternStripe>(*varPS);
    }
    else if (PatternGradient *varPS = dynamic_cast<PatternGradient *>(argPattern))
    {
        mbrPattern = std::make_unique<PatternGradient>(*varPS);
    }
    else if (PatternRing *varPS = dynamic_cast<PatternRing *>(argPattern))
    {
        mbrPattern = std::make_unique<PatternRing>(*varPS);
    }
    else {
        mbrPattern = std::make_unique<Pattern>(*argPattern);
    }
}