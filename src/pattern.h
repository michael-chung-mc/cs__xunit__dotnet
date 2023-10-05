#pragma once
#ifndef PATTERN_H
#define PATTERN_H

class Matrix;
#include "color.h"
#include "tuple.h"
#include <vector>

class Pattern {
public:
    Color mbrBlack;
    Color mbrWhite;
    std::vector<Color> mbrColors;
    Matrix* mbrTransform;
    Pattern();
    Color getColor(Point argPoint);
    void setTransform(Matrix argTransform);
};

class PatternStripe : public Pattern {
public:
    PatternStripe();
    PatternStripe(Color argColorA, Color argColorB);
    Color getColor(Point argPoint);
};

#endif