#include "pattern.h"
#include "color.h"
#include "tuple.h"
#include "matrix.h"
#include <cmath>

Pattern::Pattern()
{
    mbrWhite = Color(1,1,1);
    mbrBlack = Color(0,0,0);
    mbrTransform = new IdentityMatrix(4,4);
}
Color Pattern::getColor(Point argPoint) {
    return Color(argPoint.mbrX, argPoint.mbrY, argPoint.mbrZ);
}
void Pattern::setTransform(Matrix *argTransform) {
    delete mbrTransform;
    mbrTransform = nullptr;
    mbrTransform = new Matrix(*argTransform);
}

PatternStripe::PatternStripe() : Pattern() {
    mbrColors.push_back(mbrWhite);
    mbrColors.push_back(mbrBlack);
}
PatternStripe::PatternStripe(Color argColorA, Color argColorB) : Pattern() {
    mbrColors.push_back(argColorA);
    mbrColors.push_back(argColorB);
}
Color PatternStripe::getColor(Point argPoint) {
    return (int)std::floor(argPoint.mbrX) % 2 == 0 ? mbrColors[0] : mbrColors[1];
}