using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public static class MockDataProvider
	{

		public static void SetUpMockData ()
		{
			var connection = new TreeWatchDatabase ();
			connection.ClearDataBase ();

			var query = new DBQuery<Field> (connection);

			var blocks = new List<Block> ();
			double startlatA = 51.392712; 
			double startlatB = 51.392799;
			double startlonA = 6.055982;
			double startlonB = 6.053727;
			for (int i = 0; i < 21; i++) {
				if (i == 5) {
					blocks.Add (new Block (new  List<Position> { 
						new Position (startlatA + 0.00005, startlonA - 0.00110),
						new Position (startlatB, startlonB),
						new Position (startlatB + 0.0001, startlonB + 0.00001), 
						new Position (startlatA + 0.00015, startlonA - 0.00109)
					}, 
						TreeType.PEAR));
					blocks.Add (new Block (new  List<Position> { 
						new Position (startlatA, startlonA),
						new Position (startlatB -0.00005, startlonB + 0.00130),
						new Position (startlatB +0.00005, startlonB + 0.00131), 
						new Position (startlatA + 0.0001, startlonA + 0.00001)
					}, 
						TreeType.APPLE));
				} else {
					if (i < 5) {
						blocks.Add (new Block (new  List<Position> { 
							new Position (startlatA, startlonA),
							new Position (startlatB, startlonB),
							new Position (startlatB + 0.0001, startlonB + 0.00001), 
							new Position (startlatA + 0.0001, startlonA + 0.00001)
						}, 
							TreeType.PEAR));
					} else {
						blocks.Add (new Block (new  List<Position> { 
							new Position (startlatA, startlonA),
							new Position (startlatB, startlonB),
							new Position (startlatB + 0.0001, startlonB + 0.00001), 
							new Position (startlatA + 0.0001, startlonA + 0.00001)
						}, 
							TreeType.APPLE));
					}
				}
				startlatA += 0.000111;
				startlatB += 0.000111;
				startlonA += 0.000006;
				startlonB += 0.000018;
			}
			blocks.Add (new Block (new  List<Position> { 
				new Position (startlatA, startlonA),
				new Position (startlatB, startlonB),
				new Position (startlatA + 0.0003, startlonA + 0.00001)
			}, 
				TreeType.APPLE));

			query.InsertWithChildren (new Field ("Ajax", new List<Position> {
				new Position (51.395390, 6.056181),
				new Position (51.392672, 6.056074),
				new Position (51.392766, 6.053628),
				new Position (51.395189, 6.054014)
			}, blocks));

			query.InsertWithChildren (new Field ("PSV", new List<Position> {
				new Position (51.487109, 4.464810),
				new Position (51.486474, 4.466023),
				new Position (51.485038, 4.463276),
				new Position (51.486167, 4.461914),
				new Position (51.486454, 4.462643),
				new Position (51.486347, 4.462761)
			}, new List<Block> ()));

			query.InsertWithChildren (new Field ("Roda jc", new List<Position> {
				new Position (51.372129, 6.046075),
				new Position (51.369650, 6.047126),
				new Position (51.369476, 6.045667),
				new Position (51.369918, 6.045259),
				new Position (51.371131, 6.042325)
			}, new List<Block> ()));
				
			blocks = new List<Block> ();
			startlatA = 51.387758; 
			startlatB = 51.386690;
			startlonA = 6.040104;
			startlonB = 6.036195;
			for (int i = 0; i < 15; i++) {
				blocks.Add (new Block (new  List<Position> { 
					new Position (startlatA, startlonA),
					new Position (startlatB, startlonB),
					new Position (startlatB + 0.0001, startlonB + 0.0001), 
					new Position (startlatA + 0.0001, startlonA - 0.000075)
				}, 
					TreeType.APPLE));
				startlatA += 0.000111;
				startlatB += 0.000111;
				startlonA -= 0.000075;
				startlonB += 0.000111;
			}

			query.InsertWithChildren (new Field ("VVV", new List<Position> {
				new Position (51.387718, 6.040184),
				new Position (51.386620, 6.036065),
				new Position (51.389485, 6.038983)
			}, blocks));

			blocks = new List<Block> ();
			startlatA = 51.390508; 
			startlatB = 51.389674;
			startlonA = 6.050828;
			startlonB = 6.047850;
			for (int i = 0; i < 9; i++) {
				blocks.Add (new Block (new  List<Position> { 
					new Position (startlatA, startlonA),
					new Position (startlatB, startlonB),
					new Position (startlatB + 0.0001, startlonB), 
					new Position (startlatA + 0.0001, startlonA)
				}, 
					TreeType.APPLE));
				startlatA += 0.000111;
				startlatB += 0.000111;
			}
			startlatB = 51.391453;
			startlonB = 6.049844;

			blocks.Add (new Block (new  List<Position> { 
				new Position (startlatA, startlonA),
				new Position (startlatB - 0.000221, startlonB),
				new Position (startlatB, startlonB)
			}, 
				TreeType.APPLE));
			
			startlatA += 0.000011;
			startlatB += 0.000011;

			for (int i = 0; i < 4; i++) {
				blocks.Add (new Block (new  List<Position> { 
					new Position (startlatA, startlonA),
					new Position (startlatB, startlonB),
					new Position (startlatB + 0.000075, startlonB), 
					new Position (startlatA + 0.000075, startlonA)
				}, 
					TreeType.APPLE));
				startlatA += 0.000086;
				startlatB += 0.000086;
			}  

			query.InsertWithChildren (new Field ("Hertog Jan", new List<Position> {
				new Position (51.389619, 6.047791),
				new Position (51.391065, 6.047748),
				new Position (51.391213, 6.049744),
				new Position (51.391936, 6.049722),
				new Position (51.391922, 6.050967),
				new Position (51.390450, 6.050924)
			}, blocks));


		}
	}
}

