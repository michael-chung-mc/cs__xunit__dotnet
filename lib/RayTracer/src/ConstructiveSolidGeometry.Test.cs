using Xunit;
using LibConstructiveSolidGeometry;
using LibComparinator;
using LibProjectMeta;
using LibForm;
namespace LibConstructiveSolidGeometry.Test;


public class ConstructiveSolidGeometryTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
	public void ConstructiveSolidGeometryTestCanary_WithDefault_ExpectDefault() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void CSGConstructor_WithSphereCubeUnion_ExpectSphereCubeUnion() {
		UnitSphere varSphere = new UnitSphere();
        UnitCube varCube = new UnitCube();
		CSGOperation varOp = new CSGOperation();
		varOp._fieldUnion = true;
        ConstructiveSolidGeometry varCSG = new ConstructiveSolidGeometry(varOp, varSphere, varCube);
		Assert.True(varCSG._fieldLeft.CheckEqual(varSphere));
		Assert.True(varCSG._fieldRight.CheckEqual(varCube));
		Assert.True(varCSG._fieldOperation._fieldUnion);
		Assert.False(varCSG._fieldOperation._fieldIntersection);
		Assert.False(varCSG._fieldOperation._fieldDifference);
		Assert.Equal(varSphere._fieldParent, varCSG);
		Assert.Equal(varCube._fieldParent, varCSG);
	}
}