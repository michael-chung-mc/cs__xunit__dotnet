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
    std::vector<Color> mbrColors;
    Pattern();
    Color getColor(Point argPoint);
};

class PatternStripe : public Pattern {
public:
    PatternStripe();
    PatternStripe(Color argColorA, Color argColorB);
    Color getColor(Point argPoint);
};

#endif