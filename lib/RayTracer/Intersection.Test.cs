using Xunit;
using LibLight;
using LibForm;
using LibIntersection;
using LibComparinator;
using LibRay;
using LibTuple;
using LibPattern;
using LibProjectMeta;
using LibMaterial;
using LibMatrix;

namespace LibIntersection.Test;

public class IntersectionTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();

    [Fact]
	public void IntersectionTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void IntersectionConstructor_WithSphere_ExpectSphere() {
		UnitSphere s = new UnitSphere();
		double t = 3.5;
		Intersection i = new Intersection(t, s);
		Assert.Equal(i._fieldTime, t);
		Assert.True(i._fieldObject.CheckEqual(s));
	}
	[Fact]
	public void Intersection_WithAggregation_ExpectTwo() {
		UnitSphere s = new UnitSphere();
		double t1 = 1;
		double t2 = 2;
		Intersections i = new Intersections(t1, s);
		i.SetIntersect(t2, s);
		Assert.Equal(2, i._fieldIntersections.Count());
		Assert.Equal(i._fieldIntersections[0]._fieldTime, t1);
		Assert.Equal(i._fieldIntersections[1]._fieldTime, t2);
	}
	[Fact]
	public void Intersection_WithPositiveT_ExpectHit() {
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(1, s);
		i.SetIntersect(2, s);
		Intersection hit = i.GetHit();
		Assert.True(hit.CheckEqual(i._fieldIntersections[0]));
	}
	[Fact]
	public void Intersection_WithNegativeT_ExpectHit() {
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(-1, s);
		i.SetIntersect(2, s);
		Intersection hit = i.GetHit();
		Assert.True(hit.CheckEqual(i._fieldIntersections[1]));
	}
	[Fact]
	public void Intersection_WithAllNegativeT_ExpectHit() {
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(-1, s);
		i.SetIntersect(-2, s);
		Intersection hit = i.GetHit();
		Assert.False(hit._fieldExists);
	}
	[Fact]
	public void Intersection_WithGiven_ExpectHitNonNegative() {
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(5, s);
		i.SetIntersect(7, s);
		i.SetIntersect(-3, s);
		i.SetIntersect(2, s);
		Intersection hit = i.GetHit();
		Assert.True(hit.CheckEqual(i._fieldIntersections[1]));
	}
	[Fact]
	public void IntersectionsGetHit_WithPrecompute_ExpectGiven() {
		Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(4, s);
		IntersectionState varIs = i.GetHit().GetState(r, i._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIs._fieldTime, i.GetHit()._fieldTime));
		Assert.True(varIs._fieldObject.CheckEqual(i.GetHit()._fieldObject));
		Assert.True(_fieldComp.CheckTuple(varIs._fieldPoint, new Point(0,0,-1)));
		Assert.True(_fieldComp.CheckTuple(varIs._fieldPov, new Vector(0,0,-1)));
		Assert.True(_fieldComp.CheckTuple(varIs._fieldNormal, new Vector(0,0,-1)));
	}
	[Fact]
	public void IntersectionsGetHit_WithInteriorMiss_ExpectFalse() {
		Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(4, s);
		IntersectionState varIs = i.GetHit().GetState(r, i._fieldIntersections);
		Assert.False(varIs._fieldInside);
	}
	[Fact]
	public void IntersectionsGetHit_WithInteriorHit_ExpectTrue() {
		Ray r = new Ray(new Point(0,0,0), new Vector(0,0,1));
		UnitSphere s = new UnitSphere();
		Intersections i = new Intersections(1, s);
		IntersectionState varIs = i.GetHit().GetState(r, i._fieldIntersections);
		Assert.True(_fieldComp.CheckTuple(varIs._fieldPoint, new Point(0,0,1)));
		Assert.True(_fieldComp.CheckTuple(varIs._fieldPov, new Vector(0,0,-1)));
		Assert.True(varIs._fieldInside);
		Assert.True(_fieldComp.CheckTuple(varIs._fieldNormal, new Vector(0,0,-1)));
	}
	[Fact]
	public void IntersectionsGetHit_WithGiven_ExpectOffsetPoint() {
		Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(0,0,1));
		Intersections varIntersection = new Intersections(5, varSphere);
		IntersectionState varvarIs = varIntersection.GetHit().GetState(varRay, varIntersection._fieldIntersections);
		Assert.True(varvarIs._fieldOverPoint._fieldZ < -varPM.GetEpsilon()/2);
		Assert.True(varvarIs._fieldPoint._fieldZ > varvarIs._fieldOverPoint._fieldZ);
	}
	[Fact]
	public void IntersectionsGetHit_WithReflection_ExpectReflect()
	{
		UnitPlane varObj = new UnitPlane();
		Ray varRay = new Ray(new Point(0,1,-1), new Vector(0,-Math.Sqrt(2)/2, Math.Sqrt(2)/2));
		Intersections varIx = new Intersections(Math.Sqrt(2), varObj);
		IntersectionState varvarIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
		Assert.True(_fieldComp.CheckTuple(varvarIs._fieldReflect, new Vector(0,Math.Sqrt(2)/2, Math.Sqrt(2)/2)));
	}
	[Fact]
	public void IntersectionsGetHit_WithRefractiveIndex_ExpectBouncedHits () {
		UnitSphereGlass varSphereA = new UnitSphereGlass();
		varSphereA.SetTransform(new ScalingMatrix(2,2,2));
		varSphereA._fieldMaterial._fieldRefractiveIndex = 1.5;
		UnitSphereGlass varSphereB = new UnitSphereGlass();
		varSphereB.SetTransform(new TranslationMatrix(0,0,-0.25));
		varSphereB._fieldMaterial._fieldRefractiveIndex = 2.0;
		UnitSphereGlass varSphereC = new UnitSphereGlass();
		varSphereC.SetTransform(new TranslationMatrix(0,0,0.25));
		varSphereC._fieldMaterial._fieldRefractiveIndex = 2.5;
		Ray argRay = new Ray(new Point(0,0,-4), new Vector(0,0,1));
		Intersections varvarIs = new Intersections();
		varvarIs.SetIntersect(2, new UnitSphereGlass(varSphereA));
		varvarIs.SetIntersect(2.75, new UnitSphereGlass(varSphereB));
		varvarIs.SetIntersect(3.25, new UnitSphereGlass(varSphereC));
		varvarIs.SetIntersect(4.75, new UnitSphereGlass(varSphereB));
		varvarIs.SetIntersect(5.25, new UnitSphereGlass(varSphereC));
		varvarIs.SetIntersect(6, new UnitSphereGlass(varSphereA));
		IntersectionState varIx0 = varvarIs._fieldIntersections[0].GetState(argRay, varvarIs._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx0._fieldRefractiveIndexOne, 1.0));
		Assert.True(_fieldComp.CheckFloat(varIx0._fieldRefractiveIndexTwo, 1.5));
		IntersectionState varIx1 = varvarIs._fieldIntersections[1].GetState(argRay, varvarIs._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx1._fieldRefractiveIndexOne, 1.5));
		Assert.True(_fieldComp.CheckFloat(varIx1._fieldRefractiveIndexTwo, 2.0));
		IntersectionState varIx2 = varvarIs._fieldIntersections[2].GetState(argRay, varvarIs._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx2._fieldRefractiveIndexOne, 2.0));
		Assert.True(_fieldComp.CheckFloat(varIx2._fieldRefractiveIndexTwo, 2.5));
		IntersectionState varIx3 = varvarIs._fieldIntersections[3].GetState(argRay, varvarIs._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx3._fieldRefractiveIndexOne, 2.5));
		Assert.True(_fieldComp.CheckFloat(varIx3._fieldRefractiveIndexTwo, 2.5));
		IntersectionState varIx4 = varvarIs._fieldIntersections[4].GetState(argRay, varvarIs._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx4._fieldRefractiveIndexOne, 2.5));
		Assert.True(_fieldComp.CheckFloat(varIx4._fieldRefractiveIndexTwo, 1.5));
		IntersectionState varIx5 = varvarIs._fieldIntersections[5].GetState(argRay, varvarIs._fieldIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx5._fieldRefractiveIndexOne, 1.5));
		Assert.True(_fieldComp.CheckFloat(varIx5._fieldRefractiveIndexTwo, 1.0));
	}
	[Fact]
	public void IntersectionsGetHit_WithGiven_ExpectUnderPointUnderSurfaceByEpsilon ()
	{
		Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		UnitSphereGlass varObj = new UnitSphereGlass();
		varObj.SetTransform(new TranslationMatrix(0,0,1));
		Intersections varIx = new Intersections(5, varObj);
		IntersectionState varvarIs = varIx.GetHit().GetState(varRay, varIx._fieldIntersections);
		Assert.True(varvarIs._fieldUnderPoint._fieldZ > varPM.GetEpsilon()/2);
		Assert.True(varvarIs._fieldPoint._fieldZ < varvarIs._fieldUnderPoint._fieldZ);
	}
	[Fact]
	public void IntersectionsGetSchlickApproximation_WithTotalInternalReflection_ExpectOne () {
		UnitSphereGlass varObj = new UnitSphereGlass();
		Ray varRay = new Ray(new Point(0,0,Math.Sqrt(2)/2), new Vector(0,1,0));
		Intersections varIs = new Intersections();
		varIs.SetIntersect(-Math.Sqrt(2)/2, varObj);
		varIs.SetIntersect(Math.Sqrt(2)/2, varObj);
		IntersectionState varIx = varIs._fieldIntersections[1].GetState(varRay, varIs._fieldIntersections);
		double varReflectance = varIx.GetSchlick();
		Assert.Equal(1.0, varReflectance);
	}
	[Fact]
	public void IntersectionsGetSchlickApproximation_WithPerpendicularViewingAngle_ExpectSmall () {
		UnitSphereGlass varObj = new UnitSphereGlass();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,1,0));
		Intersections varIs = new Intersections();
		varIs.SetIntersect(-1, varObj);
		varIs.SetIntersect(1, varObj);
		IntersectionState varIx = varIs._fieldIntersections[1].GetState(varRay, varIs._fieldIntersections);
		double varReflectance = varIx.GetSchlick();
		Assert.True(_fieldComp.CheckFloat(0.04, varReflectance));
	}
	[Fact]
	public void IntersectionsGetSchlickApproximation_WithSmallViewingAngle_ExpectLarge () {
		UnitSphereGlass varObj = new UnitSphereGlass();
		Ray varRay = new Ray(new Point(0,0.99,-2), new Vector(0,0,1));
		Intersections varIs = new Intersections(1.8589, varObj);
		IntersectionState varIx = varIs._fieldIntersections[0].GetState(varRay, varIs._fieldIntersections);
		double varReflectance = varIx.GetSchlick();
		Assert.True(_fieldComp.CheckFloat(0.48873, varReflectance));
	}
	[Fact]
	public void IntersectionConstructor_WithTriangle_ExpectNormalized() {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Intersection varIs = new Intersection(3.5, varObj, 0.2, 0.4);
		Assert.Equal(0.2, varIs._fieldU);
		Assert.Equal(0.4, varIs._fieldV);
		Intersections varIx = new Intersections();
		varIx.SetIntersect(3.5,varObj, 0.2, 0.4);
		Assert.Equal(0.2, varIx._fieldIntersections[0]._fieldU);
		Assert.Equal(0.4, varIx._fieldIntersections[0]._fieldV);
	}
}