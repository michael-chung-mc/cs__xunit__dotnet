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