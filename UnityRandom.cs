using UnityEngine;
using System;
using NPack;
using URandom;

/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// Unity Random                                                            //
//                                                                         //
// This code is free software under the Artistic license.                  //
//                                                                         //
// distributions from: http://www.nrbook.com/a/bookcpdf.php                //
//                                                                         //
// TODO:                                                                   //
// 1. Shuffle Bag                                                          //
// 2. Random in a sphere with normalizations [?]                           //
// 3. Dices (1d6, 2d6)                                                     //
// 4. Random colors                                                        //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////

public class UnityRandom 
{
	public enum Normalization
	{
		STDNORMAL = 0,
		POWERLAW = 1
	}
		
	private MersenneTwister _rand;
	
	// THE CONSTRUCTORS
	public UnityRandom()
    { 
		_rand = new MersenneTwister();
	}
	
	public UnityRandom(int seed)
	{
		_rand = new MersenneTwister(seed);
	}
	
	// INTERFACE:
	
	// Return a Float 0 - 1
	public float Value()
	{
		return _rand.NextSingle(true);
	}
	
	// Return a Float 0 - 1
	public float Value( Normalization n , float t)
	{
		if (n == Normalization.STDNORMAL) {
			return (float) NormalDistribution.Normalize(_rand.NextSingle(true), t);
		} else if (n == Normalization.POWERLAW) {
			return (float) PowerLaw.Normalize(_rand.NextSingle(true), t, 0, 1);
		} else {
			return _rand.NextSingle(true);
		}
	}
	
	// Return a Float min < x < max
	public float Range(Int32 minValue, Int32 maxValue)
	{
		return _rand.Next(minValue, maxValue);
	}
		
	// Return a Float min < x < max
	public float Range(Int32 minValue, Int32 maxValue, Normalization n, float t)
	{
		if (n == Normalization.STDNORMAL) {
			return SpecialFunctions.ScaleFloatToRange( (float) NormalDistribution.Normalize(_rand.NextSingle(true), t), minValue, maxValue, 0, 1);
		} else if (n == Normalization.POWERLAW) {
			return (float) PowerLaw.Normalize(_rand.NextSingle(true), t, minValue, maxValue);
		} else {
			return _rand.Next(minValue, maxValue);
		}
	}	
	
	public float Possion(float lambda)
	{
		return PoissonDistribution.Normalize( ref _rand, lambda);
	}
	
	public float Exponential(float lambda)
	{
		return ExponentialDistribution.Normalize( _rand.NextSingle( false ), lambda );
	}
	
	public float Gamma(float order)
	{
		return GammaDistribution.Normalize(ref _rand, (int) order);
	}
	
	// RANDOM POINT IN A SQUARE
	public Vector2 PointInASquare()
	{
		return RandomSquare.Area(ref _rand);
	}
	
	public Vector2 PointInASquare(Normalization n , float t )
	{
		return RandomSquare.Area(ref _rand, n, t);
	}
	
	// RANDOM POINT IN A CIRCLE centered at 0
	// FROM http://mathworld.wolfram.com/CirclePointPicking.html
	// Take a number between 0 and 2PI and move to Cartesian Coordinates
	public Vector2 PointInACircle()
	{			
		return RandomDisk.Circle(ref _rand);
	}
	
	public Vector2 PointInACircle(Normalization n, float t)
	{			
		return RandomDisk.Circle(ref _rand, n, t);
	}
	
	// RANDOM POINT in a DISK
	// FROM http://mathworld.wolfram.com/DiskPointPicking.html
	public Vector2 PointInADisk()
	{
		return RandomDisk.Disk(ref _rand);
	}
	
	public Vector2 PointInADisk(Normalization n, float t)
	{
		return RandomDisk.Disk(ref _rand, n, t);
	}
		
	public Vector3 PointInACube()
	{
		return RandomCube.Volume(ref _rand);
	}
	
	public Vector3 PointInACube(Normalization n, float t)
	{
		return RandomCube.Volume(ref _rand, n, t);
	}
	
	public Vector3 PointOnACube()
	{
		return RandomCube.Surface(ref _rand);
	}
	
	public Vector3 PointOnACube(Normalization n, float t)
	{
		return RandomCube.Surface(ref _rand, n, t);
	}
	
	public Vector3 PointOnASphere()
	{
		return RandomSphere.Surface(ref _rand);
	}
	
	public Vector3 PointOnASphere(Normalization n, float t)
	{
		throw new ArgumentException("Normalizations for Sphere is not yet implemented");
	}
	
	public Vector3 PointInASphere()
	{
		return RandomSphere.Volume(ref _rand);
	}
	
	public Vector3 PointInASphere(Normalization n, float t)
	{
		throw new ArgumentException("Normalizations for Sphere is not yet implemented");
	}
}
