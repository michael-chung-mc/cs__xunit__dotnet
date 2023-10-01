#include "pch.h"

Material::Material ()
{
    color = Color(1,1,1);
    ambient = 0.1;
    diffuse = 0.9;
    specular = 0.9;
    shininess = 200.0;
}