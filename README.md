# wip__cpp__c__gtest
<a name="readme-top"></a>
<details>
    <summary>Table of Contents</summary>
    <ol>
        <li><a href="#goals">Goals</a>
            <ul>
                <li><a href="#about">About</li>
                <li><a href="#preview">Preview</li>
            </ul>
        </li>
        <li><a href="#methodology">Methodology</li>
          <ul>
            <li><a href="#requirements">Requirements</li>
            <li><a href="#design">Design</li>
            <li><a href="#tools">Tools</li>
            <li><a href="#roadmap">Roadmap</li>
          </ul>
        </li>
        <li><a href="#usage">Usage</a>
            <ul>
                <li><a href="#setup">Setup</li>
                <li><a href="#run">Run</li>
            </ul>
        </li>
        <li><a href="#acknowledgements">Acknowledgements</li>
    </ol>
</details>

## Goals
### About
Ray Tracer implemented in C# following Test Driven Development methodology using xUnit
### Preview
![Refraction-Air-Pocket-Background-Checker-Example](./examples/refraction__sphere_with_air_pocket__checker.png)
![Refraction-Air-Pocket-Background-Ring-Example](./examples/refraction__sphere_with_air_pocket__ring.png)
![Refraction-Example](./examples/refraction.png)
![Reflection-Refraction-Example](./examples/reflection_refraction__scene.png)
![Cube-Room-Made-Of-Cubes-Example](./examples/cube_room_made_of_aabb_cubes.png)
![Cylinders-Example](./examples/cylinders.png)
## Methodology
### Requirements
### Design
### Tools
* C#
* xUnit
* Visual Studio Code
* Git
* Xubuntu
### Roadmap
<details>
<summary>ray tracer</summary>

- [x] Operations
    - [x] Tuples & Point & Vector
    - [x] Matrix
        - [x] Translation
        - [x] Scale
        - [x] Rotate
        - [x] Shear
    - [x] Rays
- [x] Geometry
    - [x] Sphere(s)
    - [x] Plane(s)
    - [x] AABB = Axis Aligned Bounding Box(es)
    - [x] Cylinder(s)
    - [x] DNC = Double Napped Cone(s)
    - [ ] Object Groups
    - [ ] Triangle
    - [ ] CSG = Constructive Solid Geometry
    - [ ] Torus
- [x] Material
    - [x] Lighting
        - [x] Phong Model
        - [x] Shadow
        - [x] Reflection
        - [x] Refraction
        - [x] Fresnel
            -[x] Schlick
    - [x] Pattern
        - [x] Striped Pattern
        - [x] Gradient Pattern
        - [x] Ring Pattern
        - [x] Checkered Pattern
        - [ ] Radial Gradient
        - [ ] Nested Patterns
        - [ ] Blended Patterns
        - [ ] Preturbed Patterns
    - [ ] Texture Map
- [x] World
    - [x] Canvas
        - [x] PPM
    - [x] Camera
        - [ ] Focal Blur
        - [ ] Motion Blur
    - [x] Lights
        - [x] Point Source
        - [ ] Area Light
        - [ ] Spotlight
- [x] Render
    - [x] Single Object Shadow/Silhouette Cast Render
    - [x] Single Matte Shading Render
    - [x] Multiple Matte Shading Render
    - [x] Single Transparent Render
    - [x] Single Refraction Render
    - [x] Multiple Matte/Transparent/Refraction Render

</details>

## Usage
### Setup
### Run
## Acnowledgements
* "Ray Tracer Challenge" by Jamis Buck
* "VisualStudio.gitignore" by Github
<p align="right">(<a href="#readme-top">back to top </a>)</p>
