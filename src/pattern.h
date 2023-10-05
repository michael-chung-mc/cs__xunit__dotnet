#pragma once
#ifndef PATTERN_H
#define PATTERN_H

#include "color.h"
#include "tuple.h"
#include <vector>

class Pattern {
public:
    Color mbrBlack;
    Color mbrWhite;
    Pattern();
};

class PatternStripe : public Pattern {
public:
    std::vector<Color> mbrColors;
    PatternStripe();
    PatternStripe(Color argColorA, Color argColorB);
    Color getStripe(Point argPoint);
};

#endif