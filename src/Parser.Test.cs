using Xunit;
using LibComparinator;
using LibParser;
using LibTuple;
using LibForm;
namespace LibParser.Test;

public class ParserTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void Canary()
    {
        Assert.Equal(1, 1);
    }
}

public class WaveFrontObjParserTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void Canary()
    {
        Assert.Equal(1, 1);
    }
    [Fact]
    public void ParseWaveFrontObj_With_NonWaveFrontFormattedFiveLines_Expect_FiveSkippedLines () {
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        String varData = "There was a young lady named Bright\nwho traveled much faster than light.\nShe set out one day\nin a relative way,\nand came back the previous night.";
        int SkippedLines = varParser.ParseWaveFrontObj(varData);
        Assert.Equal(5, SkippedLines);
    }
    [Fact]
    public void ParseWaveFrontObj_With_WaveFrontFormattedFourVertices_Expect_ContainFourVertices () {
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        String varData = "v -1 1 0\nv -1.000 0.5000 0.000\nv 1 0 0\nv 1 1 0";
        int SkippedLines = varParser.ParseWaveFrontObj(varData);
        Assert.Equal(5, varParser._fieldVertices.Count);
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[1],new Point (-1,1,0)));
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[2],new Point (-1,0.5,0)));
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[3],new Point (1,0,0)));
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[4],new Point (1,1,0)));
    }
    [Fact]
    public void ParseWaveFrontObj_With_WaveFrontFormattedFourVerticesTwoFaces_Expect_ContainTwoFacesReferencingFourVertices () {
        ParserWaveFrontObj varParser = new ParserWaveFrontObj();
        String varData = "v -1 1 0\nv -1 0 0\nv 1 0 0\nv 1 1 0\nf 1 2 3\nf 1 3 4";
        int SkippedLines = varParser.ParseWaveFrontObj(varData);
        Assert.Equal(5, varParser._fieldVertices.Count);
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[1],new Point (-1,1,0)));
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[2],new Point (-1,0,0)));
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[3],new Point (1,0,0)));
        Assert.True(_fieldComp.CheckTuple(varParser._fieldVertices[4],new Point (1,1,0)));
        Assert.Equal(2, varParser._fieldGroup._fieldForms.Count);
        Form varFaceOne = varParser._fieldGroup._fieldForms[0];
        Form varFaceTwo = varParser._fieldGroup._fieldForms[1];
        Assert.True(_fieldComp.CheckTuple(varFaceOne._fieldForms[0]._fieldVertexOne, varParser._fieldVertices[1]));
        Assert.True(_fieldComp.CheckTuple(varFaceOne._fieldForms[0]._fieldVertexTwo, varParser._fieldVertices[2]));
        Assert.True(_fieldComp.CheckTuple(varFaceOne._fieldForms[0]._fieldVertexThree, varParser._fieldVertices[3]));
        Assert.True(_fieldComp.CheckTuple(varFaceTwo._fieldForms[0]._fieldVertexOne, varParser._fieldVertices[1]));
        Assert.True(_fieldComp.CheckTuple(varFaceTwo._fieldForms[0]._fieldVertexTwo, varParser._fieldVertices[3]));
        Assert.True(_fieldComp.CheckTuple(varFaceTwo._fieldForms[0]._fieldVertexThree, varParser._fieldVertices[4]));
    }
}