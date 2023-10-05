#pragma once
#ifndef PATTERN_H
#define PATTERN_H

class Matrix;
#include "color.h"
#include "tuple.h"
#include <vector>
#include <memory>

class Pattern {
public:
    Color mbrBlack;
    Color mbrWhite;
    std::vector<Color> mbrColors;
    std::unique_ptr<Matrix> mbrTransform;
    Pattern();
	Pattern(const Pattern& other);
    virtual ~Pattern();
    virtual Color getColorLocal(Point argPoint);
    void setTransform(const Matrix &argMatrix);
};

class PatternStripe : public Pattern {
public:
    PatternStripe();
	PatternStripe(const PatternStripe& other);
    PatternStripe(Color argColorA, Color argColorB);
    Color getColorLocal(Point argPoint) override;
};

#endif