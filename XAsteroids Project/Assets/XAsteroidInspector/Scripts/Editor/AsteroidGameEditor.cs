///////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using System.Collections;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace MoonPincho
{
	[CustomEditor(typeof(AsteroidGame))]
	public class AsteroidGameEditor : Editor
	{
		//*****************************************************************************************************
		bool InitFlag = true;
		bool StoreFlag = false;
		float AsteroidLogoFlash = 5.0f;
		bool SpeedItemFlag = false;
		bool EnergyItemFlag = false;
		bool LaserItemFlag = false;
		bool GameOverFlag = false;
		//-----------------------------------------------------------------------------------------------------
		Texture MoonLogoTexture;
		Texture BackgroundTexture;
		Texture AsteroidLogoTexture;
		Texture ShipTexture;
		Texture ShipWithFlameTexture;
		Texture Asteroid1Texture;
		Texture Asteroid2Texture;
		Texture Asteroid1TextureHigh;
		Texture Asteroid2TextureHigh;
		Texture LaserTexture;
		Texture ScoreTexture;
		Texture MoneyTexture;
		Texture LaserLineTexture;
		Texture EnergyLineTexture;
		Texture SpeedLogoTexture;
		Texture StoreTexture;
		Texture SpeedItemTexture;
		Texture EnergyItemTexture;
		Texture LaserItemTexture;
		Texture GameOverTexture;
		Texture[] CoinTextureArray;
		Texture[] ExplosionTextureArray;
		Texture[] NumberTextureArray;
		//-----------------------------------------------------------------------------------------------------
		AudioClip CoinClip;
		AudioClip LaserClip;
		AudioClip ExplosionClip;
		//-----------------------------------------------------------------------------------------------------
		AudioSource Audio;
		//-----------------------------------------------------------------------------------------------------
		int Money = 0;
		int Score = 0;
		//-----------------------------------------------------------------------------------------------------
		float Width = 0;
		float Height = 0;
		int IntroAnimation = 0;
		float SpaceShipPositionX = 370;
		float SpaceShipPositionY = 1200;
		int SpaceShipEnergy = 500;
		int SpaceShipLaser = 500;
		float SpaceShipSpeed = 300;
		bool SpaceShipTurnLeftFlag = false;
		bool SpaceShipTurnRightFlag = false;
		bool SpaceShipForwardFlag = false;
		bool SpaceShipBackwardFlag = false;
		bool SpaceShipFlameFlag = false;
		//-----------------------------------------------------------------------------------------------------
		LIST<LASER> LaserList;
		LIST<ASTEROID> AsteroidList;
		LIST<EXPLOSION> ExplosionList;
		LIST<COIN> CoinList;
		//-----------------------------------------------------------------------------------------------------
		float LaserSpeed = 2000;
		float LaserShotTime = 0;
		float LaserShotRepeatTime = 0.8f;
		//-----------------------------------------------------------------------------------------------------
		float ExplosionSpeed = 150;
		float ExplosionAnimationSpeed = 30.0f;
		//-----------------------------------------------------------------------------------------------------
		float CoinSpeed = 300;
		float CoinAnimationSpeed = 15.0f;
		//-----------------------------------------------------------------------------------------------------
		float AsteroidSpeed = 300;
		float AsteroidCreationTime = 0;
		float AsteroidRepeatTime = 3;
		float AsteroidEnergy = 0.8f;
		int AsteroidHighTime = 10;
		//-----------------------------------------------------------------------------------------------------
		long DeltaTimeTickOld = 0;
		long DeltaTimeTick = 0;
		float DeltaTime = 0;
		//*****************************************************************************************************

		//-----------------------------------------------------------------------------------------------------
		void Init()
		{
			if (InitFlag)
			{
				InitFlag = false;

				BackgroundTexture = Resources.Load("Texture/BackgroundTexture") as Texture;
				AsteroidLogoTexture = Resources.Load("Texture/AsteroidLogoTexture") as Texture;
				ShipTexture = Resources.Load("Texture/ShipTexture") as Texture;
				ShipWithFlameTexture = Resources.Load("Texture/ShipWithFlameTexture") as Texture;
				Asteroid1Texture = Resources.Load("Texture/Asteroid1Texture") as Texture;
				Asteroid2Texture = Resources.Load("Texture/Asteroid2Texture") as Texture;
				Asteroid1TextureHigh = Resources.Load("Texture/Asteroid1TextureHigh") as Texture;
				Asteroid2TextureHigh = Resources.Load("Texture/Asteroid2TextureHigh") as Texture;
				LaserTexture = Resources.Load("Texture/LaserTexture") as Texture;
				ScoreTexture = Resources.Load("Texture/ScoreTexture") as Texture;
				MoneyTexture = Resources.Load("Texture/MoneyTexture") as Texture;
				SpeedLogoTexture = Resources.Load("Texture/SpeedLogoTexture") as Texture;
				StoreTexture = Resources.Load("Texture/StoreTexture") as Texture;
				SpeedItemTexture = Resources.Load("Texture/SpeedItem") as Texture;
				EnergyItemTexture = Resources.Load("Texture/EnergyItem") as Texture;
				LaserItemTexture = Resources.Load("Texture/LaserItem") as Texture;
				GameOverTexture = Resources.Load("Texture/GameOverTexture") as Texture;

				LaserLineTexture = Resources.Load("Texture/LaserLineTexture") as Texture;
				EnergyLineTexture = Resources.Load("Texture/EnergyLineTexture") as Texture;

				//Load Coin Textures
				CoinTextureArray = new Texture[10];
				for (int i = 0; i < 10; i++)
				{
					CoinTextureArray[i] = Resources.Load("Texture/Coin/Coin" + i.ToString()) as Texture;
				}

				//Load Explosion Textures
				ExplosionTextureArray = new Texture[32];
				for (int i = 0; i < 32; i++)
				{
					ExplosionTextureArray[i] = Resources.Load("Texture/Explosion/Explosion" + i.ToString()) as Texture;
				}

				//Load Numbers Texture
				NumberTextureArray = new Texture[10];
				NumberTextureArray[0] = Resources.Load("Texture/Numbers/Number0") as Texture;
				NumberTextureArray[1] = Resources.Load("Texture/Numbers/Number1") as Texture;
				NumberTextureArray[2] = Resources.Load("Texture/Numbers/Number2") as Texture;
				NumberTextureArray[3] = Resources.Load("Texture/Numbers/Number3") as Texture;
				NumberTextureArray[4] = Resources.Load("Texture/Numbers/Number4") as Texture;
				NumberTextureArray[5] = Resources.Load("Texture/Numbers/Number5") as Texture;
				NumberTextureArray[6] = Resources.Load("Texture/Numbers/Number6") as Texture;
				NumberTextureArray[7] = Resources.Load("Texture/Numbers/Number7") as Texture;
				NumberTextureArray[8] = Resources.Load("Texture/Numbers/Number8") as Texture;
				NumberTextureArray[9] = Resources.Load("Texture/Numbers/Number9") as Texture;

				//Init List
				LaserList = new LIST<LASER>();
				AsteroidList = new LIST<ASTEROID>();
				ExplosionList = new LIST<EXPLOSION>();
				CoinList = new LIST<COIN>();

				//DeltaTime Refresh
				System.DateTime time = System.DateTime.Now;
				DeltaTimeTickOld = time.Ticks;
				DeltaTimeTick = time.Ticks;
				DeltaTime = 0;

				//Sound
				LaserClip = Resources.Load("Sound/LaserSound") as AudioClip;
				ExplosionClip = Resources.Load("Sound/ExplosionSound") as AudioClip;
				CoinClip = Resources.Load("Sound/CoinSound") as AudioClip;

				Audio = ((AsteroidGame)target).gameObject.GetComponent<AudioSource>();
				if (Audio == null) ((AsteroidGame)target).gameObject.AddComponent<AudioSource>();
			}
		}
		//-----------------------------------------------------------------------------------------------------
		public override void OnInspectorGUI()
		{
			Init();

			GUILayout.Space(8);

			Width = Mathf.Min(Screen.width, BackgroundTexture.width);
			Height = BackgroundTexture.height * Width / BackgroundTexture.width;

			GUILayout.Label(BackgroundTexture, GUILayout.Width(Width), GUILayout.Height(Height));

			if (StoreFlag)
			{
				//Store Texture
				DrawSprite(StoreTexture, 0, 350, 900, 400);

				//Score Logo Draw
				DrawSprite(ScoreTexture, 50, 30, 200, 200);

				//Money Logo Draw
				DrawSprite(MoneyTexture, 630, 30, 200, 200);

				//Energy Line
				DrawSprite(EnergyLineTexture, 0, 1520 - SpaceShipEnergy, 150, 20);
				//Energy Value 
				DrawSpriteString(SpaceShipEnergy.ToString(), 40, Mathf.Max(0,1590 - SpaceShipEnergy), 200, 70, Color.grey);

				//Laser Line
				DrawSprite(LaserLineTexture, 750, 1520 - SpaceShipLaser, 150, 20);
				//Laser Value 
				DrawSpriteString(SpaceShipLaser.ToString(), 780, Mathf.Max(0,1590 - SpaceShipLaser), 70, 70, Color.grey);

				//Speed Logo
				DrawSprite(SpeedLogoTexture, 340, 1500, 200, 40);
				//Speed Value 
				DrawSpriteString(SpaceShipSpeed.ToString(), 470, 1515, 200, 70, Color.red);

				//Score Number
				DrawNumber(Score, 9, new Rect(60, 100, 32, 70));
				//Money Number
				DrawNumber(Money, 9, new Rect(540, 100, 32, 70));

				//Speed Item
				SpeedItemFlag = DrawSpriteButton(SpeedItemTexture, 0, 800, 900, 40);
				//Energy Item
				EnergyItemFlag = DrawSpriteButton(EnergyItemTexture, 0, 1000, 900, 40);
				//Laser Item
				LaserItemFlag = DrawSpriteButton(LaserItemTexture, 0, 1200, 900, 40);

				GUI.backgroundColor = Color.white;

	/*			SpeedItemFlag = GUILayout.Button(SpeedItemTexture, GUILayout.Width(Width), GUILayout.Height(32));
				EnergyItemFlag = GUILayout.Button(EnergyItemTexture, GUILayout.Width(Width), GUILayout.Height(32));
				LaserItemFlag = GUILayout.Button(LaserItemTexture, GUILayout.Width(Width), GUILayout.Height(32));
*/
				StoreFlag = !GUILayout.Button("BACK TO THE GAME", GUILayout.Width(Width), GUILayout.Height(32));
				if (!StoreFlag)
				{
					//DeltaTime Init
					System.DateTime time = System.DateTime.Now;
					DeltaTimeTickOld = time.Ticks;
					DeltaTimeTick = time.Ticks;
					DeltaTime = 0;
				}

				StoreUpdate();

				//Store Update
				if (Event.current.type == EventType.repaint)
				{
					StoreUpdate();
				}
			}
			else
			{
				//Asteroid Logo Draw
				if (AsteroidLogoFlash > 0)
				{
					DrawSprite(AsteroidLogoTexture, 0, 230, 900, 300,new Color(1,1,1,AsteroidLogoFlash));
				}

				//Score Logo Draw
				DrawSprite(ScoreTexture, 50, 30, 200, 200);

				//Money Logo Draw
				DrawSprite(MoneyTexture, 630, 30, 200, 200);

				//Energy Line
				DrawSprite(EnergyLineTexture, 0, 1520 - SpaceShipEnergy, 150, 20);
				//Energy Value 
				DrawSpriteString(SpaceShipEnergy.ToString(), 40,Mathf.Max(0,1590 - SpaceShipEnergy), 200, 70,Color.grey);

				//Laser Line
				DrawSprite(LaserLineTexture, 750, 1520 - SpaceShipLaser, 150, 20);
				//Laser Value 
				DrawSpriteString(SpaceShipLaser.ToString(), 780, Mathf.Max(0,1590 - SpaceShipLaser), 70, 70,Color.grey);
	
				//Speed Logo
				DrawSprite(SpeedLogoTexture, 340, 1500, 200, 40);
				//Speed Value 
				DrawSpriteString(SpaceShipSpeed.ToString(), 470, 1515, 200, 70,Color.red);

				//Draw Ship
				if (SpaceShipFlameFlag) DrawSprite(ShipWithFlameTexture, SpaceShipPositionX, SpaceShipPositionY, 128, 200);
				else DrawSprite(ShipTexture, SpaceShipPositionX, SpaceShipPositionY, 128, 200);

				//Draw Laser
				int count = LaserList.GetCount();
				for (int i = 0; i < count; i++)
				{
					DrawSprite(LaserTexture, LaserList[i].PositionX, LaserList[i].PositionY, 32, 32);
				}

				//Draw Asteroid
				Texture texture = null;
				count = AsteroidList.GetCount();
				for (int i = 0; i < count; i++)
				{
					ASTEROID asteroid = AsteroidList[i];

					if (asteroid.Type == 1)
					{
						if (asteroid.HighRate > 0) texture = Asteroid1TextureHigh;
						else texture = Asteroid1Texture;
					}
					if (asteroid.Type == 2)
					{
						if (asteroid.HighRate > 0) texture = Asteroid2TextureHigh;
						else texture = Asteroid2Texture;
					}

					DrawSprite(texture, asteroid.PositionX, asteroid.PositionY, 256, 256);
				}

				//Draw Coin
				count = CoinList.GetCount();
				for (int i = 0; i < count; i++)
				{
					COIN coin = CoinList[i];
					DrawSprite(CoinTextureArray[(int)coin.AnimationFrame], coin.PositionX, coin.PositionY, 100, 100);
				}

				//Draw Explosion
				count = ExplosionList.GetCount();
				for (int i = 0; i < count; i++)
				{
					EXPLOSION explosion = ExplosionList[i];
					DrawSprite(ExplosionTextureArray[(int)explosion.AnimationFrame], explosion.PositionX, explosion.PositionY, 300, 300);
				}

				//Score Number
				DrawNumber(Score, 9, new Rect(60, 100, 32, 70));
				//Money Number
				DrawNumber(Money, 9, new Rect(540, 100, 32, 70));

				//Game Over
				if(GameOverFlag) DrawSprite(GameOverTexture, 30, 400, 830, 900);

				if (GameOverFlag)
				{
					GameOverFlag = !GUILayout.Button("START NEW GAME", GUILayout.Width(Width), GUILayout.Height(32));
				}
				else
				{
					StoreFlag = GUILayout.Button("STORE", GUILayout.Width(Width), GUILayout.Height(32));
				}

				//Game Update
				if (Event.current.type == EventType.repaint)
				{
					GameUpdate();
				}
				else
				{
					//Left Arrow Key 
					if ((Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.A))
					{
						SpaceShipTurnLeftFlag = true;
					}
					if ((Event.current.type == EventType.KeyUp) && (Event.current.keyCode == KeyCode.A))
					{
						SpaceShipTurnLeftFlag = false;
					}

					//Right Arrow Key
					if ((Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.D))
					{
						SpaceShipTurnRightFlag = true;
					}
					if ((Event.current.type == EventType.KeyUp) && (Event.current.keyCode == KeyCode.D))
					{
						SpaceShipTurnRightFlag = false;
					}

					//Up Arrow Key
					if ((Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.W))
					{
						SpaceShipForwardFlag = true;
					}
					if ((Event.current.type == EventType.KeyUp) && (Event.current.keyCode == KeyCode.W))
					{
						SpaceShipForwardFlag = false;
					}

					//Down Arrow Key
					if ((Event.current.type == EventType.KeyDown) && (Event.current.keyCode == KeyCode.S))
					{
						SpaceShipBackwardFlag = true;
					}
					if ((Event.current.type == EventType.KeyUp) && (Event.current.keyCode == KeyCode.S))
					{
						SpaceShipBackwardFlag = false;
					}

					//Left Mouse Button Down
					if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
					{
						SpaceShipTurnLeftFlag = true;
					}
					if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
					{
						SpaceShipTurnLeftFlag = false;
					}
					//Right Mouse Button Down
					if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
					{
						SpaceShipTurnRightFlag = true;
					}
					if (Event.current.type == EventType.MouseUp && Event.current.button == 1)
					{
						SpaceShipTurnRightFlag = false;
					}

					/*			//Mouse Position
								Vector2 position = Event.current.mousePosition;
								EditorGUILayout.FloatField("Mouse X", position.x);
								EditorGUILayout.FloatField("Mouse Y", position.y);
					*/

				}
			}

			//Line
			GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            //M-12 Logo and Rights
            MoonLogoTexture = Resources.Load("Texture/MoonLogo") as Texture;
			GUILayoutOption[] option = { GUILayout.Width(100), GUILayout.Height(100) };

			GUILayout.Space(8);

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Box(MoonLogoTexture, option);
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.Space(4);

			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label("Moon Pincho. 2016");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();

			GUILayout.Space(8);

			//Repaint
			Repaint();
		}
		//-----------------------------------------------------------------------------------------------------
		void StoreUpdate()
		{
			//Laser Item
			if (LaserItemFlag)
			{
				LaserItemFlag = false;

				if (Money >= 3)
				{
					Money -= 3;
					SpaceShipLaser += 10;

					//Coin Sound Play
					Audio.PlayOneShot(CoinClip);
				}
			}

			//Energy Item
			if (EnergyItemFlag)
			{
				EnergyItemFlag = false;
				if (Money >= 1)
				{
					Money -= 1;
					SpaceShipEnergy += 10;

					//Coin Sound Play
					Audio.PlayOneShot(CoinClip);
				}
			}

			//Speed Item
			if (SpeedItemFlag)
			{
				SpeedItemFlag = false;

				if (Money >= 5)
				{
					Money -= 5;
					SpaceShipSpeed += 5;

					//Coin Sound Play
					Audio.PlayOneShot(CoinClip);
				}
			}
		}
		//-----------------------------------------------------------------------------------------------------
		void GameUpdate()
		{
			//DeltaTime Refresh
			System.DateTime time = System.DateTime.Now;
			DeltaTimeTickOld = DeltaTimeTick;
			DeltaTimeTick = time.Ticks;
			DeltaTime = (float) (DeltaTimeTick - DeltaTimeTickOld) / System.TimeSpan.TicksPerSecond;

			//Asteroid Logo Flash
			if (AsteroidLogoFlash > 0)
			{
				AsteroidLogoFlash -= 1 * DeltaTime;
			}

			//SpaceShip Turn
			if (SpaceShipTurnLeftFlag) SpaceShipPositionX -= SpaceShipSpeed * DeltaTime;
			if (SpaceShipPositionX < 0) SpaceShipPositionX = 0;
			if (SpaceShipTurnRightFlag) SpaceShipPositionX += SpaceShipSpeed * DeltaTime;
			if (SpaceShipPositionX > 760) SpaceShipPositionX = 760;
			if (SpaceShipForwardFlag) SpaceShipPositionY -= SpaceShipSpeed * DeltaTime;
			if (SpaceShipPositionY < 100) SpaceShipPositionY = 100;
			if (SpaceShipBackwardFlag) SpaceShipPositionY += SpaceShipSpeed * DeltaTime;
			if (SpaceShipPositionY > 1300) SpaceShipPositionY = 1300;

			//SpaceShip Flame
			if (SpaceShipFlameFlag) SpaceShipFlameFlag = false;
			else SpaceShipFlameFlag = true;

			//Intro
			IntroAnimation++;
			if (IntroAnimation > 480) IntroAnimation = 0;

			if (SpaceShipLaser > 0)
			{
				//LaserShot
				LaserShotTime += DeltaTime;
				if (LaserShotTime > LaserShotRepeatTime)
				{
					LaserShotTime -= LaserShotRepeatTime;
					LASER laser = new LASER();
					laser.PositionX = SpaceShipPositionX + 50;
					laser.PositionY = SpaceShipPositionY - 50;

					LaserList.Add(laser);

					Audio.PlayOneShot(LaserClip, 0.3f);

					if (SpaceShipLaser > 1000)
					{
						LASER laser1 = new LASER();
						laser.PositionX = SpaceShipPositionX + 30;
						laser.PositionY = SpaceShipPositionY - 50;
						LASER laser2 = new LASER();
						laser.PositionX = SpaceShipPositionX + 70;
						laser.PositionY = SpaceShipPositionY - 50;

						LaserList.Add(laser1);
						LaserList.Add(laser2);

						//Laser Fuel
						if (SpaceShipLaser > 0) SpaceShipLaser -= 3;
					}
					else
					{
						//Laser Fuel
						if (SpaceShipLaser > 0) SpaceShipLaser--;
					}
				}
			}

			//Laser Update
			int count = LaserList.GetCount();
			for (int i = 0; i < count; i++)
			{
				LaserList[i].PositionY -= LaserSpeed * DeltaTime;
				if (LaserList[i].PositionY <= 0) LaserList.RemoveIndex(i);
			}

			//Asteroid LEVEL UP
			if (AsteroidRepeatTime > 0.1f) AsteroidRepeatTime -= 0.000001f;
			AsteroidSpeed += 0.000001f;
			AsteroidEnergy += 0.000001f;

			//Asteroid Field Creation
			AsteroidCreationTime += DeltaTime;
			if (AsteroidCreationTime > AsteroidRepeatTime)
			{
				AsteroidCreationTime -= AsteroidRepeatTime;
				ASTEROID asteroid = new ASTEROID();
				asteroid.Energy = AsteroidEnergy;
				asteroid.HighRate = 0;
				asteroid.Type = Random.Range(1, 3);
				asteroid.PositionX = Random.Range(0, 640);
				asteroid.PositionY = 0;
				AsteroidList.Add(asteroid);
			}

			//Asteroid Update
			count = AsteroidList.GetCount();
			for (int i = 0; i < count; i++)
			{
				ASTEROID asteroid = AsteroidList[i];

				float end = 0;
				if (asteroid.Type == 1) end = 1420;
				if (asteroid.Type == 2) end = 1300;
				asteroid.PositionY += AsteroidSpeed * DeltaTime;
				if (asteroid.PositionY > end) AsteroidList.RemoveIndex(i);
				if (asteroid.HighRate > 0) asteroid.HighRate--;
			}

			//Coin Update
			count = CoinList.GetCount();
			for (int i = 0; i < count; i++)
			{
				COIN coin = CoinList[i];

				coin.AnimationFrame += CoinAnimationSpeed * DeltaTime;
				if (coin.AnimationFrame > 9) coin.AnimationFrame -= 9;
				coin.PositionY += CoinSpeed * DeltaTime;
				if (coin.PositionY > 1450) CoinList.RemoveIndex(i);
			}

			//Explosion Update
			count = ExplosionList.GetCount();
			for (int i = 0; i < count; i++)
			{
				EXPLOSION explosion = ExplosionList[i];

				explosion.PositionY += ExplosionSpeed * DeltaTime;
				if (explosion.PositionY > 1300) ExplosionList.RemoveIndex(i);
				explosion.AnimationFrame += ExplosionAnimationSpeed * DeltaTime;
				if (explosion.AnimationFrame > 31) ExplosionList.RemoveIndex(i);
			}

			//SpaceShip Update
			if (SpaceShipEnergy <= 0)
			{
				//Game Over
				GameOverFlag = true;
			}

			if (!GameOverFlag)
			{
				AsteroidLaserCollision();
				AsteroidSpaceShipCollision();
				CoinSpaceShipCollision();
			}
		}
		//-----------------------------------------------------------------------------------------------------
		public void DrawNumber(int number, int digitCount, Rect area)
		{
			int n = number;
			int count = 0;
			do
			{
				n = n / 10;
				count++;
			}
			while (n > 0);

			for (int i = 0; i < digitCount - count; i++)
			{
				DrawSprite(NumberTextureArray[0], area.x + i * area.width, area.y, area.width, area.height);
			}

			for (int i = count; i > 0; i--)
			{
				n = number % 10;
				number = number / 10;
				DrawSprite(NumberTextureArray[n], area.x + (i + digitCount - count - 1) * area.width, area.y, area.width, area.height);		
			}
		}
		//-----------------------------------------------------------------------------------------------------
		bool DrawSpriteButton(Texture texture, float x, float y, float width, float height)
		{
			x = x * Width / 900;
			y = (1600 - y) * Height / 1600;
			width = width * Width / 900;
			height = Mathf.Max(height * Height / 1600, texture.height);

			GUI.color = Color.white;
			GUI.backgroundColor = new Color(1,1,1,0);

			GUILayout.Space(-y);
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(x));
			bool flag = GUILayout.Button(texture, GUILayout.Width(width), GUILayout.Height(height));
			GUILayout.EndHorizontal();
			GUILayout.Space(y - height - 2);

			return flag;
		}
		//-----------------------------------------------------------------------------------------------------
		void DrawSpriteString(string str, float x, float y, float width, float height,Color color)
		{
			x = x * Width / 900;
			y = (1600 - y) * Height / 1600;
			width = width * Width / 900;
			height = height * Height / 1600;

			GUIStyle style = new GUIStyle();
			style.normal.textColor = color;
			style.fontStyle = FontStyle.Bold;

			GUILayout.Space(-y);
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(x));
			GUILayout.Label(str,style, GUILayout.Width(width), GUILayout.Height(height));
			GUILayout.EndHorizontal();
			GUILayout.Space(y - height - 2);
		}
		//-----------------------------------------------------------------------------------------------------
		void DrawSprite(Texture texture, float x, float y, float width, float height)
		{
			x = x * Width / 900;
			y = (1600 - y) * Height / 1600;
			width = width * Width / 900;
			height = Mathf.Max(height * Height / 1600, texture.height);

			GUI.color = Color.white;

			GUILayout.Space(-y);
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(x));
			GUILayout.Label(texture, GUILayout.Width(width), GUILayout.Height(height));
			GUILayout.EndHorizontal();
			GUILayout.Space(y - height - 2);
		}
		//-----------------------------------------------------------------------------------------------------
		void DrawSprite(Texture texture, float x, float y, float width, float height, Color color)
		{
			x = x * Width / 900;
			y = (1600 - y) * Height / 1600;
			width = width * Width / 900;
			height = Mathf.Max(height * Height / 1600, texture.height);

			GUI.color = color;

			GUILayout.Space(-y);
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(x));
			GUILayout.Label(texture, GUILayout.Width(width), GUILayout.Height(height));
			GUILayout.EndHorizontal();
			GUILayout.Space(y - height - 2);
		}
		//-----------------------------------------------------------------------------------------------------
		void AsteroidLaserCollision()
		{
			int count = LaserList.GetCount();
			for (int i = 0; i < count; i++)
			{
				int count2 = AsteroidList.GetCount();
				for (int j = 0; j < count2; j++)
				{
					ASTEROID asteroid = AsteroidList[j];

					float radius = 0;
					if (asteroid.Type == 1) radius = 100;
					if (asteroid.Type == 2) radius = 120;

					Vector2 laserPos = new Vector2(LaserList[i].PositionX + 4, LaserList[i].PositionY + 8);
					Vector2 asteroidPos = new Vector2(asteroid.PositionX + radius, asteroid.PositionY + radius);
					float distance = Vector2.Distance(laserPos, asteroidPos);

					//Collision
					if (distance < 10 + radius)
					{

						asteroid.HighRate = AsteroidHighTime;
						asteroid.Energy -= 1.0f;
						if (asteroid.Energy <= 0)
						{
							//Add Explosion
							EXPLOSION explosion = new EXPLOSION();
							explosion.PositionX = asteroid.PositionX;
							explosion.PositionY = asteroid.PositionY;
							ExplosionList.Add(explosion);

							//Explosion Sound Play
							if (asteroid.Type == 1) Audio.PlayOneShot(ExplosionClip, 0.5f);
							if (asteroid.Type == 2) Audio.PlayOneShot(ExplosionClip);

							//Add Coin
							COIN coin = new COIN();
							coin.PositionX = asteroid.PositionX;
							coin.PositionY = asteroid.PositionY;
							CoinList.Add(coin);

							//Add Explosion Score
							Score += 100;

							//Asteroid Remove
							AsteroidList.RemoveIndex(j);
						}
						else
						{
							//Add Hit Score
							Score += 10;
						}
						LaserList.RemoveIndex(i);

					}
				}
			}
		}
		//-----------------------------------------------------------------------------------------------------
		void AsteroidSpaceShipCollision()
		{
			int count = AsteroidList.GetCount();
			for (int i = 0; i < count;i++ )
			{
				ASTEROID asteroid = AsteroidList[i];

				Vector2 spaceShipPos = new Vector2(SpaceShipPositionX + 60, SpaceShipPositionY + 30);
				Vector2 asteroidPos = new Vector2(asteroid.PositionX + 100, asteroid.PositionY + 100);
				float distance = Vector2.Distance(spaceShipPos, asteroidPos);

				//Collision
				if (distance < 160)
				{

					asteroid.HighRate = AsteroidHighTime;
					asteroid.Energy -= 1.0f;
					if (asteroid.Energy <= 0)
					{
						//Add Explosion
						EXPLOSION explosion = new EXPLOSION();
						explosion.PositionX = asteroid.PositionX;
						explosion.PositionY = asteroid.PositionY;
						ExplosionList.Add(explosion);

						//Explosion Sound Play
						Audio.PlayOneShot(ExplosionClip);

						//Asteroid Remove
						AsteroidList.RemoveIndex(i);
					}

					//SpaceShip Collision
					SpaceShipEnergy -= 10;
				}
			}
		}
		//-----------------------------------------------------------------------------------------------------
		void CoinSpaceShipCollision()
		{
			int count = CoinList.GetCount();
			for (int i = 0; i < count;i++ )
			{
				Vector2 spaceShipPos = new Vector2(SpaceShipPositionX + 60 , SpaceShipPositionY + 30);
				Vector2 coinPos = new Vector2(CoinList[i].PositionX + 20, CoinList[i].PositionY + 20);
				float distance = Vector2.Distance(spaceShipPos, coinPos);

				//Collision
				if (distance < 80)
				{
					//Coin Sound Play
					Audio.PlayOneShot(CoinClip);

					//Remove Coin from List
					CoinList.RemoveIndex(i);

					//Add Money
					Money += 1;
				}
			}
		}
		//-----------------------------------------------------------------------------------------------------
		class LASER
		{
			public float PositionX;
			public float PositionY;
		}
		//-----------------------------------------------------------------------------------------------------
		class COIN
		{
			public float PositionX;
			public float PositionY;
			public float AnimationFrame;
		}
		//-----------------------------------------------------------------------------------------------------
		class EXPLOSION
		{
			public float PositionX;
			public float PositionY;
			public float AnimationFrame;
		}
		//-----------------------------------------------------------------------------------------------------
		class ASTEROID
		{
			public float PositionX;
			public float PositionY;
			public int Type;
			public int HighRate;
			public float Energy;
		}
		//-----------------------------------------------------------------------------------------------------
	}
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////



