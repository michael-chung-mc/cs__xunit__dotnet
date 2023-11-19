using Xunit;
using LibForm;
using LibTuple;
using LibRay;
using LibMaterial;
using LibMatrix;
using LibComparinator;
using LibIntersection;
using LibProjectMeta;

namespace LibForm.Test;

public class FormTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void FormTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void FormConstructor_WithDefault__ExpectEmptyParent() {
		Form varObj = new Form();
		Assert.Null(varObj._fieldParent);
	}
    [Fact]
	public void FormWorldToObjectSpaceConversion_WithGiven_ExpectTransformed() {
		CompositeGroup varGroupInner = new CompositeGroup();
		varGroupInner.SetTransform(new ScalingMatrix(2,2,2));
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		varGroupInner.SetObject(varSphere);
		Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
		CompositeGroup varGroupOuter = new CompositeGroup();
		varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
		varGroupOuter.SetObject(varGroupInner);
		Assert.True(varGroupOuter._fieldForms[0]._fieldTransform.CheckEqual(varGroupInner._fieldTransform));
		Point varPoint = new Point(-2,0,-10);
		Point varExpectedPoint = new Point(0,0,-1);
		Assert.True(_fieldComp.CheckTuple(varExpectedPoint, varGroupOuter._fieldForms[0]._fieldForms[0]._fieldTransform.GetInverse() * varGroupOuter._fieldForms[0]._fieldTransform.GetInverse() * varGroupOuter._fieldTransform.GetInverse() * varPoint));
		SpaceTuple varTransformedPoint = varGroupOuter._fieldForms[0]._fieldForms[0].GetObjectPointFromWorldSpace(varPoint);
		Assert.True(_fieldComp.CheckTuple(varTransformedPoint, varExpectedPoint));
	}
    [Fact]
	public void FormObjectToWorldNormalConversion_WithGiven_ExpectTransformed() {
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		CompositeGroup varGroupInner = new CompositeGroup();
		varGroupInner.SetTransform(new ScalingMatrix(1,2,3));
		varGroupInner.SetObject(varSphere);
		Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
		CompositeGroup varGroupOuter = new CompositeGroup();
		varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
		varGroupOuter.SetObject(varGroupInner);
		Vector varNormal = new Vector(Math.Sqrt(3)/3, Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		SpaceTuple varTransformedNormal = varGroupOuter._fieldForms[0]._fieldForms[0].GetWorldNormalFromObjectSpace(varNormal);
		Vector varExpectedNormal = new Vector(0.2857, 0.4286, -0.8571);
		Assert.True(_fieldComp.CheckTuple(varTransformedNormal, varExpectedNormal));
	}
    [Fact]
	public void FormChildNormalWithinGroup_WithGiven_ExpectTransformed() {
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		CompositeGroup varGroupInner = new CompositeGroup();
		varGroupInner.SetTransform(new ScalingMatrix(1,2,3));
		varGroupInner.SetObject(varSphere);
		Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
		CompositeGroup varGroupOuter = new CompositeGroup();
		varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
		varGroupOuter.SetObject(varGroupInner);
		Point varPoint = new Point(1.7321, 1.1547, -5.5774);
		SpaceTuple varTransformedNormal = varGroupOuter._fieldForms[0]._fieldForms[0].GetNormal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(0.2857, 0.4286, -0.8571);
		Assert.True(_fieldComp.CheckTuple(varTransformedNormal, varExpectedNormal));
	}
}
public class SphereTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void SphereTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void SphereEquality_WithEqual_ExpectTrue() {
		UnitSphere s = new UnitSphere();
		UnitSphere t = new UnitSphere();
		Assert.True(s.CheckEqual(t));
	}
    [Fact]
	public void SphereRayIntersect_BeforeSphere_ExpectTwoFront() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereRayIntersect_WithTangent_ExpectTwo() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(5 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(5 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereRayIntersect_WithMiss_ExpectEmpty() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Empty(varXs._fieldIntersections);
	}
    [Fact]
	public void SphereRayIntersect_WithinSphere_ExpectFrontBackIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, 0),new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.Equal(-1 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(1 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereRayIntersect_BehindSphere_ExpectTwoBackIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.Equal(-6 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(-4 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereRayIntersect_AheadObject_ExpectSetsObject() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.True(s.CheckEqual((varXs._fieldIntersections[0]._fieldObject)));
		Assert.True(s.CheckEqual((varXs._fieldIntersections[1]._fieldObject)));
	}
    [Fact]
	public void Sphere_WithDefault_ExpectTransformIsIdentity() {
		UnitSphere s = new UnitSphere();
		IdentityMatrix m = new IdentityMatrix(4, 4);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}
    [Fact]
	public void SphereModifyTransform_WithGiven_ExpectTransformed() {
		UnitSphere s = new UnitSphere();
		TranslationMatrix m = new TranslationMatrix(2, 3, 4);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}
    [Fact]
	public void Sphere_WithIdentity_ExpectDoesNotModifyIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(1, 1, 1);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void Sphere_WithScaledTransform_ExpectModifiesIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(2, 2, 2);
		s.SetTransform(m);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(3, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(7, varXs._fieldIntersections[1]._fieldTime);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(0,0,-2.5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,0.5)));
	}
    [Fact]
	public void Sphere_WithScaledFiveTimes_ExpectIntersections() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(5, 5, 5);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		Intersections varXs = s.GetIntersections(r);
		double z = 0;
		double y = 10;
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[0]._fieldTime, z));
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[1]._fieldTime, y));
	}
    [Fact]
	public void Sphere_WithTranslatedAway_ExpectMiss() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(5, 0, 0);
		s.SetTransform(new Matrix(m));
		Intersections varXs = s.GetIntersections(r);
		Assert.Empty(varXs._fieldIntersections);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(-5,0,-5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,1)));
	}
    [Fact]
	public void Sphere_withTranslatedAway_ExpectHit() {
		UnitSphere s = new UnitSphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(0, 0, 1);
		s.SetTransform(m);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(5, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(7, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void SphereGetNormal_WithDefault_ExpectX() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(1,0,0);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		Vector expectedV = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereGetNormal_WithDefault_ExpectY() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(0,1,0);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		Vector expectedV = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereGetNormal_WithDefault_ExpectZ() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(0,0,1);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		Vector expectedV = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereGetNormal_WithDefault_ExpectPerpendicular() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		Vector expectedV = new Vector(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereGetNormal_WithDefault_ExpectNormalizedVector() {
		UnitSphere s = new UnitSphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		SpaceTuple expectedV = normal.GetNormal();
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereGetNormal_WithTranslated_ExpectNormalized() {
		UnitSphere s = new UnitSphere();
		Matrix t = new TranslationMatrix(0,1,0);
		s.SetTransform(t);
		Point p = new Point(0, 1.70711, -0.70711);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		Vector expectedV = new Vector(0, 0.70711, -0.70711);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereGetNormal_WithTransformed_ExpectNormalized() {
		UnitSphere s = new UnitSphere();
		// Matrix t = *(ScalingMatrix(1, 0.5, 1) * ZRotationMatrix(getPI()/5));
		Matrix t = new ScalingMatrix(1, 0.5, 1) * new ZRotationMatrix(varPM.GetPI()/5);
		s.SetTransform(t);
		Point p = new Point(0,Math.Sqrt(2)/2,-Math.Sqrt(2)/2);
		SpaceTuple normal = s.GetNormal(p, new Intersection());
		Vector expectedV = new Vector(0,0.97014,-0.24254);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

    [Fact]
	public void SphereMaterial_WithDefault_ExpectDefaultMaterial() {
		UnitSphere s = new UnitSphere();
		Material m = new Material();
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}

    [Fact]
	public void SphereMaterial_WithAssignment_ExpectAssignedMaterial() {
		UnitSphere s = new UnitSphere();
		Material m = new Material();
		m._fieldAmbient = 1;
		s.SetMaterial(m);
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}
}
public class SphereGlassTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void SphereGlassTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

    [Fact]
	public void GlassSphereConstructor_WithDefault_ExpectDefault() {
		UnitSphereGlass varObj = new UnitSphereGlass();
		Assert.True(varObj._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
		Assert.Equal(1.0, varObj._fieldMaterial._fieldTransparency);
		Assert.Equal(1.5, varObj._fieldMaterial._fieldRefractiveIndex);
	}
}
public class PlaneTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void PlaneTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

    [Fact]
	public void PlaneGetNormal_WithDefault_ExpectSameEverywhere() {
		UnitPlane varPlane = new UnitPlane();
		SpaceTuple varN1 = varPlane.GetNormal(new Point(0,0,0), new Intersection());
		SpaceTuple varN2 = varPlane.GetNormal(new Point(10,0,-10), new Intersection());
		SpaceTuple varN3 = varPlane.GetNormal(new Point(-5,0,150), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varN1, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN2, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN3, new Vector(0,1,0)));

	}

    [Fact]
	public void PlaneGetIntersections_WithRayParallelToPlane_ExpectMiss() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,10,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}

    [Fact]
	public void PlaneGetIntersections_WithCoplanarRayToPlane_ExpectMiss() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}
    [Fact]
	public void PlaneGetIntersections_WithRayIntersectingPlaneAbove_ExpectMiss() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,1,0), new Vector(0,-1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
    [Fact]
	public void PlaneGetIntersections_WithRayIntersectingPlaneBelow_ExpectMiss() {
		UnitPlane varPlane = new UnitPlane();
		Ray varRay = new Ray(new Point(0,-1,0), new Vector(0,1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
}
public class CubeTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CubeTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void CubeGetRayIntersect_WithPositiveX_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(5,0.5,0), new Vector(-1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithNegativeX_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(-5,0.5,0), new Vector(1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithPositiveY_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(.5,5,0), new Vector(0,-1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithNegativeY_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(.5,-5,0), new Vector(0,1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithPositiveZ_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(.5,0,5), new Vector(0,0,-1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithNegativeZ_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(.5,0,-5), new Vector(0,0,1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithInterior_ExpectTwoHits() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(0,0.5,0), new Vector(0,0,1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(-1, varXs[0]._fieldTime);
		Assert.Equal(1, varXs[1]._fieldTime);
	}
    [Fact]
	public void CubeGetRayIntersect_WithAwayX_ExpectMiss() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(-2,0,0), new Vector(0.2673,0.5345,0.8018));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CubeGetRayIntersect_WithAwayY_ExpectMiss() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(0,-2,0), new Vector(0.8018,0.2673,0.5345));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CubeGetRayIntersect_WithAwayZ_ExpectMiss() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(0,0,-2), new Vector(0.5345, 0.8018,0.2673));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CubeGetRayIntersect_WithParallelXZ_ExpectMiss() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(2,0,2), new Vector(0,0,-1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CubeGetRayIntersect_WithParallelYZ_ExpectMiss() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(0,2,2), new Vector(0,-1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CubeGetRayIntersect_WithParallelXY_ExpectMiss() {
		UnitCube varCube = new UnitCube();
		Ray varRay = new Ray(new Point(2,2,0), new Vector(-1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CubeGetNormal_WithDefault_ExpectPositiveX() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(1,0.5,-0.8);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithDefault_ExpectNegativeX() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(-1,-0.2,0.9);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(-1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithDefault_ExpectPositiveY() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(-0.4,1,-0.1);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithDefault_ExpectNegativeY() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(0.3,-1,1);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(0,-1,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithDefault_ExpectPositiveZ() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(-0.6,0.3,1);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithDefault_ExpectNegativeZ() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(0.4,0.4,-1);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(0,0,-1);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithCorner_ExpectX() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(1,1,1);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void CubeGetNormal_WithNegativeCorner_ExpectNegativeX() {
		UnitCube varCube = new UnitCube();
		Point varPoint = new Point(-1,-1,-1);
		SpaceTuple varNormal = varCube.GetNormalLocal(varPoint, new Intersection());
		Vector varExpectedNormal = new Vector(-1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
}

public class CylinderTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CylinderTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void CylinderGetIntersection_WithOnSurface_ExpectMiss() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varDirection = new Vector(0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(1,0,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CylinderGetIntersection_WithInsideSurface_ExpectMiss() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varDirection = new Vector(0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,0,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CylinderGetIntersection_AwaySurface_ExpectMiss() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varDirection = new Vector(1,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void CylinderGetIntersection_WithTangent_ExpectTwoIntersections() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(1,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(5, varXs[0]._fieldTime);
		Assert.Equal(5, varXs[1]._fieldTime);
	}
    [Fact]
	public void CylinderGetIntersection_WithPerpendicular_ExpectTwoIntersections() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void CylinderGetIntersection_WithSkewed_ExpectTwoIntersections() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varDirection = new Vector(0.1,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0.5,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(6.80798, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(7.08872, varXs[1]._fieldTime));
	}
    [Fact]
	public void CylinderGetNormal_AtX_ExpectPositiveX() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(1,0,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(1,0,0)));
	}
    [Fact]
	public void CylinderGetNormal_AtNegativeZ_ExpectNegativeZ() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(0,5,-1), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(0,0,-1)));
	}
    [Fact]
	public void CylinderGetNormal_AtPositiveZ_ExpectPositiveZ() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(0,-2,1), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(0,0,1)));
	}
    [Fact]
	public void CylinderGetNormal_AtNegativeX_ExpectNegativeX() {
		UnitCylinder varObj = new UnitCylinder();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(-1,1,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(-1,0,0)));
	}
    [Fact]
	public void Cylinder_WithDefault_ExpectHeightInfinity() {
		UnitCylinder varObj = new UnitCylinder();
		Assert.Equal(double.MaxValue, varObj._fieldHeightMax);
		Assert.Equal(double.MinValue, varObj._fieldHeightMin);
	}
    [Fact]
	public void Cylinder_WithHeightConstrainedAngledRay_ExpectIntersectionMiss() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		SpaceTuple varDirection = new Vector(0.1,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,1.5,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_WithHeightConstrainedAboveRay_ExpectIntersectionMiss() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,3,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_WithHeightConstrainedBelowRay_ExpectIntersectionMiss() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_WithHeightConstrainedRayAtMaxHeight_ExpectIntersectionMiss() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,2,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_WithHeightConstrainedRayAtMinHeight_ExpectIntersectionMiss() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,1,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cylinder_WithHeightConstrained_ExpectIntersectionHit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		SpaceTuple varDirection = new Vector(0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,1.5,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Cylinder_WithDefault_ExpectClosedCylinderDefault() {
		UnitCylinder varObj = new UnitCylinder();
		Assert.False(varObj._fieldClosed);
	}
    [Fact]
	public void Cylinder_WithHeightConstrained_ExpectIntersectionAboveMiddleHit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector(0,-1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,3,0), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Cylinder_WithHeightConstrained_ExpectIntersectionAboveHit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector(0,-1,2).GetNormal();
		Ray varRay = new Ray(new Point(0,3,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Cylinder_WithHeightConstrained_ExpectIntersectionAboveCornerHit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector(0,-1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,4,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Cylinder_WithHeightConstrained_ExpectIntersectionBelowHit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector(0,1,2).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void Cylinder_WithHeightConstrained_ExpectIntersectionBelowCornerHit() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector(0,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,-1,-2), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void CylinderGetNormal_WithHeightConstrained_ExpectNormalNegativeY() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = varObj.GetNormalLocal(new Point(0,1,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,-1,0)));
	}
    [Fact]
	public void CylinderGetNormal_WithHeightConstrained_ExpectBelowRightNormalNegativeY() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = varObj.GetNormalLocal(new Point(0.5,1,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,-1,0)));
	}
    [Fact]
	public void CylinderGetNormal_WithHeightConstrained_ExpectBelowLeftNormalNegativeY() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = varObj.GetNormalLocal(new Point(0,1,0.5), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,-1,0)));
	}
    [Fact]
	public void CylinderGetNormal_WithHeightConstrained_ExpectAboveNormalPositiveY() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = varObj.GetNormalLocal(new Point(0,2,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,1,0)));
	}
    [Fact]
	public void CylinderGetNormal_WithHeightConstrained_ExpectAboveRightNormalPositiveY() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = varObj.GetNormalLocal(new Point(0.5,2,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,1,0)));
	}
    [Fact]
	public void CylinderGetNormal_WithHeightConstrained_ExpectAboveLeftNormalPositiveY() {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMax = 2;
		varObj._fieldHeightMin = 1;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = varObj.GetNormalLocal(new Point(0,2,0.5), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varDirection, new Vector(0,1,0)));
	}
}

public class DNConeTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void ConeTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void ConeGetIntersectionsLocal_WithBelowRay_ExpectHit() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varDirection = new Vector (0,0,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(5, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(5, varXs[1]._fieldTime));
	}
    [Fact]
	public void ConeGetIntersectionsLocal_WithBelowAngledRay_ExpectHit() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varDirection = new Vector (1,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(8.66025, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(8.66025, varXs[1]._fieldTime));
	}
    [Fact]
	public void ConeGetIntersectionsLocal_WithAboveAngledRay_ExpectHit() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varDirection = new Vector (-0.5,-1,1).GetNormal();
		Ray varRay = new Ray(new Point(1,1,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(4.55006, varXs[0]._fieldTime));
		Assert.True(_fieldComp.CheckFloat(49.44994, varXs[1]._fieldTime));
	}
    [Fact]
	public void ConeGetIntersectionsLocal_WithOnlyOneConeAboveAngledRay_ExpectHit() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varDirection = new Vector (0,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-1), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Single(varXs);
		Assert.True(_fieldComp.CheckFloat(0.35355, varXs[0]._fieldTime));
	}
    [Fact]
	public void Cone_Intersection__End_Cap__Miss() {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -0.5;
		varObj._fieldHeightMax = 0.5;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector (0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-5), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void ConeGetIntersectionsLocal_AtEndCap_ExpectHit() {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -0.5;
		varObj._fieldHeightMax = 0.5;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector (0,1,1).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-.25), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    [Fact]
	public void ConeGetIntersectionsLocal_AtEndCap_ExpectFourHit() {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -0.5;
		varObj._fieldHeightMax = 0.5;
		varObj._fieldClosed = true;
		SpaceTuple varDirection = new Vector (0,1,0).GetNormal();
		Ray varRay = new Ray(new Point(0,0,-.25), varDirection);
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(4, varXs.Count);
	}
    [Fact]
	public void ConeGetNormalLocal_AtOrigin_ExpectZero() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(0,0,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(0,0,0)));
	}
    [Fact]
	public void ConeGetNormalLocal_AtAbove_ExpectAbove() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(1,1,1), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(1, -Math.Sqrt(2),1)));
	}
    [Fact]
	public void ConeGetNormalLocal_AtBelow_ExpectBelow() {
		UnitDNCone varObj = new UnitDNCone();
		SpaceTuple varNormal = varObj.GetNormalLocal(new Point(-1,-1,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varNormal, new Vector(-1, 1, 0)));
	}
}

public class GroupTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Default_Group__Empty_List() {
		CompositeGroup varGroup = new CompositeGroup();
		Assert.True(varGroup._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
		Assert.Empty(varGroup._fieldForms);
	}
    [Fact]
	public void Add_Child_To_Group() {
		CompositeGroup varGroup = new CompositeGroup();
		Form varForm = new Form();
		varGroup.SetObject(varForm);
		Assert.Single(varGroup._fieldForms);
		Assert.Contains(varForm,varGroup._fieldForms);
		Assert.True(varGroup._fieldForms[0].CheckEqual(varGroup));
	}
    [Fact]
	public void Ray_Intersect_Empty_Group__No_Intersections() {
		CompositeGroup varGroup = new CompositeGroup();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
		List<Intersection> varXs = varGroup.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Intersect_Group_Of_Three_Spheres__Intersects_Two() {
		CompositeGroup varGroup = new CompositeGroup();
		UnitSphere varSphereOne = new UnitSphere();
		varGroup.SetObject(varSphereOne);
		UnitSphere varSphereTwo = new UnitSphere();
		varSphereTwo.SetTransform(new TranslationMatrix(0,0,-3));
		varGroup.SetObject(varSphereTwo);
		UnitSphere varSphereThree = new UnitSphere();
		varSphereThree.SetTransform(new TranslationMatrix(5,0,0));
		varGroup.SetObject(varSphereThree);
		Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		List<Intersection> varXs = varGroup.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(4, varXs.Count);
		Assert.True(varSphereTwo.CheckEqual(varXs[0]._fieldObject));
		Assert.True(varSphereTwo.CheckEqual(varXs[1]._fieldObject));
		Assert.True(varSphereOne.CheckEqual(varXs[2]._fieldObject));
		Assert.True(varSphereOne.CheckEqual(varXs[3]._fieldObject));
	}
    [Fact]
	public void Group_Transform_Affects_Containing_Shape() {
		CompositeGroup varGroup = new CompositeGroup();
		varGroup.SetTransform(new ScalingMatrix(2,2,2));
		UnitSphere varSphere = new UnitSphere();
		varSphere.SetTransform(new TranslationMatrix(5,0,0));
		varGroup.SetObject(varSphere);
		Ray varRay = new Ray(new Point(10,0,-10), new Vector(0,0,1));
		List<Intersection> varXs = varGroup.GetIntersections(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
	}
    // [Fact]
	// public void Group_Transform_Affects_Containing_Group() {
	// 	Group varGroupInner = new Group();
	// 	varGroupInner.SetTransform(new ScalingMatrix(2,2,2));
	// 	Sphere varSphere = new Sphere();
	// 	varSphere.SetTransform(new TranslationMatrix(5,0,0));
	// 	varGroupInner.SetObject(varSphere);
	// 	Assert.True(varGroupInner._fieldForms[0]._fieldTransform.CheckEqual(varSphere._fieldTransform));
	// 	Group varGroupOuter = new Group();
	// 	varGroupOuter.SetTransform(new YRotationMatrix(_fieldPM.GetPI()/2));
	// 	varGroupOuter.SetObject(varGroupInner);
	// 	Assert.Equal(2, varGroupOuter.GetIntersections(new Ray(new Point(10,0,-10), new Vector(0,0,1)))._fieldIntersections.Count);
	// }
}

public class TriangleTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void Default_Triangle () {
		Point varVertexOne = new Point(0,1,0);
		Point varVertexTwo = new Point(-1,0,0);
		Point varVertexThree = new Point(1,0,0);
		UnitTriangle varObj = new UnitTriangle(varVertexOne, varVertexTwo, varVertexThree);
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexOne, varVertexOne));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexTwo, varVertexTwo));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexThree, varVertexThree));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldEdgeOneTwo, new Vector(-1,-1,0)));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldEdgeOneThree, new Vector(1,-1,0)));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, new Vector(0,0,-1)));
	}
	[Fact]
	public void Triangle_Normal () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		SpaceTuple varNormalOne = varObj.GetNormalLocal(new Point(0,0.5,0), new Intersection());
		SpaceTuple varNormalTwo = varObj.GetNormalLocal(new Point(-0.5,0.75,0), new Intersection());
		SpaceTuple varNormalThree = varObj.GetNormalLocal(new Point(0.5,0.25,0), new Intersection());
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, varNormalOne));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, varNormalTwo));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormal, varNormalThree));
	}
	[Fact]
	public void Triangle__Intersection_Miss_Parallel () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Ray varRay = new Ray(new Point(0,-1,-2), new Vector(0,1,0));
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
	[Fact]
	public void Triangle__Intersection_Miss_V1_V3_Edge () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Ray varRay = new Ray(new Point(1,1,-2), new Vector(0,0,1));
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
	[Fact]
	public void Triangle__Intersection_Miss_V1_V2_Edge () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Ray varRay = new Ray(new Point(-1,1,-2), new Vector(0,0,1));
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
	[Fact]
	public void Triangle__Intersection_Miss_V2_V3_Edge () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Ray varRay = new Ray(new Point(0,-1,-2), new Vector(0,0,1));
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
	[Fact]
	public void Triangle__Intersection_Hit () {
		UnitTriangle varObj = new UnitTriangle(new Point(0,1,0), new Point(-1,0,0), new Point(1,0,0));
		Ray varRay = new Ray(new Point(0,0.5,-2), new Vector(0,0,1));
		List<Intersection> varXs = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(1, varXs.Count);
		Assert.True(_fieldComp.CheckFloat(2,varXs[0]._fieldTime));
	}
}

public class AABBTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
	[Fact]
	public void AABB_Default_Constructor__With_No_Values__Expect_Infinity () {
		AABB varBox = new AABB();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(double.MinValue,double.MinValue,double.MinValue)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(double.MaxValue,double.MaxValue,double.MaxValue)));
	}
	[Fact]
	public void AABB_Constructor__With_Values__Expect_Values () {
		Point varMin = new Point(-1,-2,-3);
		Point varMax = new Point(3,2,1);
		AABB varBox = new AABB(varMin, varMax);
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, varMin));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, varMax));
	}
	[Fact]
	public void AABB_Set_Point__With_New_Points__Expect_Resize_Min_Max () {
		AABB varBox = new AABB();
		Point varMin = new Point(-5,2,0);
		Point varMax = new Point(7,0,-3);
		varBox.SetPoint(varMin);
		varBox.SetPoint(varMax);
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-5,0,-3)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(7,2,0)));
	}
	[Fact]
	public void Sphere_AABB__With_Default__Expect_Origin_Length_1 () {
		UnitSphere varObj = new UnitSphere();
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-1,-1,-1)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(1,1,1)));
	}
	[Fact]
	public void Plane_AABB__With_Default__Expect_Origin_Length_Infinity_In_X_Z () {
		UnitPlane varObj = new UnitPlane();
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(double.MinValue, 0, double.MinValue)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(double.MaxValue, 0, double.MaxValue)));
	}
	[Fact]
	public void CubeAABB_WithDefault_ExpectOriginLength1 () {
		UnitCube varObj = new UnitCube();
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-1,-1,-1)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(1,1,1)));
	}
	[Fact]
	public void CylinderAABB_WithDefault_ExpectOriginLength1ExtendInfinityInY () {
		UnitCylinder varObj = new UnitCylinder();
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-1,double.MinValue,-1)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(1,double.MaxValue,1)));
	}
	[Fact]
	public void CylinderAABB_WithBounded3n5_ExpectOriginLength1Extend3n5 () {
		UnitCylinder varObj = new UnitCylinder();
		varObj._fieldHeightMin = -5;
		varObj._fieldHeightMax = 3;
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-1,-5,-1)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(1,3,1)));
	}
	[Fact]
	public void ConeAABB_WithDefault_ExpectOriginExtendInfinity () {
		UnitDNCone varObj = new UnitDNCone();
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(double.MinValue,double.MinValue,double.MinValue)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(double.MaxValue,double.MaxValue,double.MaxValue)));
	}
	[Fact]
	public void ConeAABB_WithBounded_n5_3_ExpectOriginLength1Extend3n5 () {
		UnitDNCone varObj = new UnitDNCone();
		varObj._fieldHeightMin = -5;
		varObj._fieldHeightMax = 3;
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-5,-5,-5)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(5,3,5)));
	}
	[Fact]
	public void TriangleAABB_WithPoints_ExpectBoundedByMinMaxPoints () {
		UnitTriangle varObj = new UnitTriangle(new Point(-3,7,2), new Point(6,2,-4), new Point(2,-1,-1));
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-3,-1,-4)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(6,7,2)));
	}
	[Fact]
	public void FormAABB_WithDfault_ExpectOriginLength1 () {
		Form varObj = new Form();
		AABB varBox = varObj.GetBounds();
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMin, new Point(-1,-1,-1)));
		Assert.True(_fieldComp.CheckTuple(varBox._fieldMax, new Point(1,1,1)));
	}
}
public class SmoothTriangleTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void SmoothTriangleConstructor_WithDefault_ExpectDefault() {
		Point varP1 = new Point(0,1,0);
		Point varP2 = new Point(-1,0,0);
		Point varP3 = new Point(1,0,0);
		Vector varN1 = new Vector(0,1,0);
		Vector varN2 = new Vector(-1,0,0);
		Vector varN3 = new Vector(1,0,0);
		SmoothTriangle varObj = new SmoothTriangle(varP1, varP2, varP3, varN1, varN2, varN3);
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexOne, varP1));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexTwo, varP2));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldVertexThree, varP3));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormalOne, varN1));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormalTwo, varN2));
		Assert.True(_fieldComp.CheckTuple(varObj._fieldNormalThree, varN3));
	}
    [Fact]
	public void SmoothTriangleSetIntersect_WithUV_ExpectStoresUV() {
		Point varP1 = new Point(0,1,0);
		Point varP2 = new Point(-1,0,0);
		Point varP3 = new Point(1,0,0);
		Vector varN1 = new Vector(0,1,0);
		Vector varN2 = new Vector(-1,0,0);
		Vector varN3 = new Vector(1,0,0);
		SmoothTriangle varObj = new SmoothTriangle(varP1, varP2, varP3, varN1, varN2, varN3);
		Ray varRay = new Ray(new Point(-0.2,0.3,-2), new Vector(0,0,1));
		List<Intersection> varIx = varObj.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.True(_fieldComp.CheckFloat(varIx[0]._fieldU, 0.45));
		Assert.True(_fieldComp.CheckFloat(varIx[0]._fieldV, 0.25));
	}
    [Fact]
	public void SmoothTriangleSetIntersect_WithUV_ExpectInterpolatesUV() {
		Point varP1 = new Point(0,1,0);
		Point varP2 = new Point(-1,0,0);
		Point varP3 = new Point(1,0,0);
		Vector varN1 = new Vector(0,1,0);
		Vector varN2 = new Vector(-1,0,0);
		Vector varN3 = new Vector(1,0,0);
		SmoothTriangle varObj = new SmoothTriangle(varP1, varP2, varP3, varN1, varN2, varN3);
		Intersection varIs = new Intersection(1,varObj, 0.45, 0.25);
		SpaceTuple varNormal = varObj.GetNormal(new Point(0,0,0), varIs);
		Assert.True(_fieldComp.CheckTuple(new Vector(-0.5547, 0.83205,0), varNormal));
	}
    [Fact]
	public void SmoothTriangleSetIntersect_WithUV_ExpectInterpolatesUVIntersectionState() {
		Point varP1 = new Point(0,1,0);
		Point varP2 = new Point(-1,0,0);
		Point varP3 = new Point(1,0,0);
		Vector varN1 = new Vector(0,1,0);
		Vector varN2 = new Vector(-1,0,0);
		Vector varN3 = new Vector(1,0,0);
		SmoothTriangle varObj = new SmoothTriangle(varP1, varP2, varP3, varN1, varN2, varN3);
		Intersections varIx = new Intersections(1,varObj, 0.45, 0.25);
		Ray varRay = new Ray(new Point(-0.2,0.3,-2), new Vector(0,0,1));
		IntersectionState varIs = varIx._fieldIntersections[0].GetState(varRay, varIx._fieldIntersections);
		Assert.True(_fieldComp.CheckTuple(new Vector(-0.5547, 0.83205,0), varIs._fieldNormal));
	}
}