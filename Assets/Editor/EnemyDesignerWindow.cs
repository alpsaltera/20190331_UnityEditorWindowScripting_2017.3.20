using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


// 2장 : 윈도우 창 띄우기
//		Scripting API
//			-> UnityEditor 쎅션.

//		using UnityEditor

//		EditorWindow 클래스 다운 받음.
//		[ MenuItem ]

//		캐스팅
//		EditorWindow.GetWindow( ~~~ ) : 윈도우를 연다. 이때 utility , title ,  focus 등을 지정해 줄 수 있다.
//		EditorWindow.minSize()
//		EditorWidnow.maxSize()
//		EditorWindow.Show()
//				utility true: 일반 윈도우 처럼 보이게 한다. , false : 탭이 있는 윈도우 처럼 보이게 한다.


// 3장 - 클래스별 기본 데이타 구조 준비
//		ScriptableObject 클래스
//		-> 생성할때 클래싀 위에 [CreateAssetMenu( fileName = "New Warrior Data" , menuName = "Character Data / Warrior" )]


// 4장 - Layoouts and Color : 윈도우의 크기를 계산해서 사각형을 윈도우 안에 그려준다.
//		-> 해더 섹션 그리기 : 		GUI.DrawTexture( headerSectionRect , headerSectionTexture );
//		Screen.width : 해당 윈도우에서의 스크린 크기. ( OnGui )안에서.
//		GUILayout.BeginArea( headerSectionRect );
//		GUILayout.EndArea();

// 5장 - 특정 위치에 어떻게 텍스트를 넣을 것인가? ( 메이지 , 워리어 , 로그 )
// 		GUILayout.Label( "Enemy Designer !" );	// GUILayout.Begin()과 End() 사이에 넣어준다. 이게 아니면, 제일 위 부터 배치됨.


// 6장 -
//		mageData.dmgType = (Types.MageDmgType) EditorGUILayout.EnumPopup( mageData.dmgType );
//		EditorGUILayout.BeginHorizontal();
//		GUILayout.과 EditorLayout의 차이점은 무엇인가?
//		GUILayout은 게임 실행시 보여주는 GUI기능에 촛점이 맞추어져 있고. ( 팝업과 같은 형태의 GUI를 제공하지 않음 )
//		GUIEditorLayout은 에디터상에서 C#와 Java같은 언어들이 제공하는 변수들을 <보여주는데> 촛점이 맞추어져 있다. ( 버튼 제작 기능이 없음 )
//		그리고 GUILayout.BeginArea()나 EditorLayout.BeginArea()는 기능이 거의 같다.

// 7장 - 버튼 액션
//		1. 새로운 윈도우를 만드는 법.
//		2. 버튼
//			if ( GUILayout.Button( "Create!" , GUILayout.Height( 40 ) ) ) {
//				GeneralSettings.OpenWindow( GeneralSettings.SettingType.Mage );
//			}
//		3. GUILayoutOption은 뭐지?
//			GUILayout.Width()와 같이 GUILayout에 관련된 함수들의 모임에 관한 집함. 이런 함수들을 모아서 한번에 보내는 것이다. 헷깔리지 말것.

// 정리
//		1. Editor와 EditorWindow 클래스 계승의 차이점.
//		2. EditorGUILayout과 GUILayout의 차이	 : Horizontalo등 및 기능 및 상징적 의미
//		3. GUILayoutOption의 의미				 : GUILayout.() 함수들을 모아 놓은 것.
//		4. 기타 함수들
//			1. EditorWindow.GetWindow( ~ )
//			1. EditorWindow.CloseWindow()
//			1. EditorWindow.minSize() maxSize();
//			1. EditorGUILayout.EnumPopup
//			1. GUI.DrawTexture()
//			1. GUILayout.Button( "Create!" , GUILayout.Height( 40 ) )
//			1. GUILayout.Space( iconSize + 8 )
//			2. GUILayout.Width()
//			3. GUILayout.BeginArea() , GUILayout.EndArea();
//			1. EditorGUILayout.FloatField( charData.maxHealth ) => 값 대입.
//			1. GUILayout.Label( "Health" );	: 가로 필드에 딱 붙여서 나타난다.
//			1. EditorGUILayout.LabelField() : 에디터에서 필요한 만큼 띄운 다음에 나타난다.
//			1. EditorGUILayout.Slizer( variable , min , max )
//			1. EditorGUILayout.TextField( charData.name );
//			1. EditorGUILayout.ObjectField( charData.prefab , typeof( GameObject ), false  );
//			1. EditorGUILayout.HelpBox( "This enemy needs a [Prefab] before it can be created." , MessageType.Warning );

//		5. EditorGUILayoutUtil. 에는 어떤 것들이 있는가?
//			1. 여러가지.

//		6. 스크립트블 에셋 만들기
//			1. ScriptableObject 클래스 계승 하기.
//			2. [CreateAssetMenu( fileName = "New Warrior Data" , menuName = "Character Data / Warrior" )]	: Scriptable 오브젝트 클래스 위에 붙이면 된다.
//			3. mageData = (MageData) ScriptableObject.CreateInstance( typeof( MageData ) ) : 클래스의New와 같은 기능
//			4. AssetDatabase
//				1. AssetDatabase.CreateAsset( EnemyDesignerWindow.MageData , dataPath );
//				2. AssetDatabase.LoadAssetAtPath( prefabPath , typeof( GameObject ) );
//				3. AssetDatabase.CopyAsset( prefabPath , newPrefabPath );
//				4. AssetDatabase.SaveAssets();
//				5. AssetDatabase.Refresh();
//
//		7. 애트리뷰트
//			1. [MenuItem("CustomWindow/Enemy Desiner")]

//		8. 커스텀 폰트 및 세부사항 넣기.
//			1. 우클릭 => 메뉴 => GUI SKIN 생성
//			2. 생성된 GUI SKIN에서 custom font 항목에 원하는 설정을 한다.
//			3. ResourceLoad() 함수사용 또는 링크 걸어주기
//			4. guiSkin = Resources.Load<GUISkin>( "guiStyles/EnemyDesignerSkin" );
//			5. 실제로 폰트를 사용할때 GUIOption 에 넣어준다.
//				GUILayout.Label( "Enemy Designer !" , guiSkin.GetStyle("Header1") );

//		9. 아이콘 표시하기.
//			1. ImageText2D 필요 (  아이콘용 )
//			2. 수작업으로 아이콘의 위치 및 크기 계산.
//			3. GUI.DrawTexture() 그려준다.
//			4. 다른 컨트롤 과의 위치적인 공간이 필요할때,
//				1. GUILayout.Space( iconSize + 8 );로 공간을 띄운다.




public class EnemyDesignerWindow : EditorWindow {

	GUISkin guiSkin;


	Texture2D headerSectionTexture;
	Texture2D mageSectionTexture;
	Texture2D warriorSectionTexture;
	Texture2D rogueSectionTexture;

	Texture2D iconTexture;
	float iconSize = 80f;
	Rect mageIconSectionRect;


	Color headerSectionColor = new Color( 13f / 255f , 32f / 255f , 44f / 255f , 1f );
	Color mageSectionColor = new Color( 1f , 1f , 1f , 1f );
	Color warriorSectionColor = new Color( 0.7f , 0.7f , 0.7f , 1f );
	Color rogueSectionColor = new Color( 0.5f , 0.5f , 0.5f , 1f );

	Rect headerSectionRect;
	Rect mageSectionRect;
	Rect warriorSectionRect;
	Rect rogueSectionRect;


	static MageData mageData;
	static WarriorData warriorData;
	static RogueData rogueData;

	public static MageData MageData { get {	return mageData; } }
	public static WarriorData WarriorData {	get	{ return warriorData; } }
	public static RogueData RogueData { get { return rogueData; } } 


	[MenuItem("CustomWindow/Enemy Desiner")]
	static void OpenWindow()
	{
		//EnemyDesignerWindow window = (EnemyDesignerWindow) GetWindow( typeof( EnemyDesignerWindow ) , true, "hellow", true );
		EnemyDesignerWindow window = (EnemyDesignerWindow) GetWindow( typeof( EnemyDesignerWindow )  );
		window.minSize = new Vector2( 300 , 600 );
		window.maxSize = new Vector2( 900 , 900 );
		//window.Show();
		window.ShowPopup();
		//window.ShowAuxWindow();	// 보여주기는 같아도 다른 곳에 포커싱을 하면 자동으로 닫힌다.
	}

	private void OnEnable()
	{
		InitTexture();
		InitData(); // 유저가 윈도우를 오픈할 때마다, Init된다.

		guiSkin = Resources.Load<GUISkin>( "guiStyles/EnemyDesignerSkin" );
	}

	public static void InitData()
	{
		mageData = (MageData) ScriptableObject.CreateInstance( typeof( MageData ) );
		warriorData = (WarriorData) ScriptableObject.CreateInstance( typeof( WarriorData ) );
		rogueData = (RogueData) ScriptableObject.CreateInstance( typeof( RogueData ) );
	}


	void InitTexture()
	{
		headerSectionTexture = new Texture2D( 1 , 1 );
		headerSectionTexture.SetPixel( 0 , 0 , headerSectionColor );
		headerSectionTexture.Apply();

		mageSectionTexture = new Texture2D( 1 , 1 );
		mageSectionTexture.SetPixel( 0 , 0 , mageSectionColor );
		mageSectionTexture.Apply();

		warriorSectionTexture = new Texture2D( 1 , 1 );
		warriorSectionTexture.SetPixel( 0 , 0 , warriorSectionColor );
		warriorSectionTexture.Apply();

		rogueSectionTexture = new Texture2D( 1 , 1 );
		rogueSectionTexture.SetPixel( 0 , 0 , rogueSectionColor );
		rogueSectionTexture.Apply();

		iconTexture = new Texture2D( 1 , 1 );
		iconTexture.SetPixel( 0 , 0 , Color.yellow );
		iconTexture.Apply();
	}

	private void OnGUI()
	{
		DrawLayouts();
		DrawHeader();
		DrawMageSettings();
		DrawWarriorSettings();
		DrawRogueSettings();
	}

	void DrawLayouts()
	{
		headerSectionRect.x = 0;
		headerSectionRect.y = 0;
		headerSectionRect.width = Screen.width;
		headerSectionRect.height = 50;


		mageSectionRect.x = 0;
		mageSectionRect.y = 50;
		mageSectionRect.width = Screen.width / 3f;
		mageSectionRect.height = Screen.width - 50f;




		mageIconSectionRect.x = (mageSectionRect.x + mageSectionRect.width / 2f - iconSize / 2f);
		mageIconSectionRect.y = mageSectionRect.y + 8;
		mageIconSectionRect.width = iconSize;
		mageIconSectionRect.height = iconSize;




		warriorSectionRect.x = Screen.width / 3f;
		warriorSectionRect.y = 50;
		warriorSectionRect.width = Screen.width / 3f;
		warriorSectionRect.height = Screen.width - 50f;

		rogueSectionRect.x = Screen.width / 3f * 2f;
		rogueSectionRect.y = 50;
		rogueSectionRect.width = Screen.width / 3f;
		rogueSectionRect.height = Screen.width - 50f;

		GUI.DrawTexture( headerSectionRect , headerSectionTexture );
		GUI.DrawTexture( mageSectionRect , mageSectionTexture );
		GUI.DrawTexture( warriorSectionRect , warriorSectionTexture );
		GUI.DrawTexture( rogueSectionRect , rogueSectionTexture );
		GUI.DrawTexture( mageIconSectionRect , iconTexture );
	}

	void DrawHeader()
	{
		GUILayout.BeginArea( headerSectionRect );
		GUILayout.Label( "Enemy Designer !" , guiSkin.GetStyle("Header1") );
		GUILayout.EndArea();
	}

	void DrawMageSettings()
	{

		// GUILayout.과 EditorLayout의 차이점은 무엇인가?
		// GUILayout은 게임 실행시 보여주는 GUI기능에 촛점이 맞추어져 있고. ( 팝업과 같은 형태의 GUI를 제공하지 않음 )
		// GUIEditorLayout은 에디터상에서 C#와 Java같은 언어들이 제공하는 변수들을 <보여주는데> 촛점이 맞추어져 있다. ( 버튼 제작 기능이 없음 )
		// 그리고 GUILayout.BeginArea()나 EditorLayout.BeginArea()는 기능이 거의 같다.

		GUILayout.BeginArea( mageSectionRect );

		GUILayout.Space( iconSize + 8 );
		GUILayout.Label( "Mage !" );

		EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "Damage" );
			mageData.dmgType = (Types.MageDmgType) EditorGUILayout.EnumPopup( mageData.dmgType );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "Weapon" );
			mageData.wpnType = (Types.MageWpnType) EditorGUILayout.EnumPopup( mageData.wpnType );
		EditorGUILayout.EndHorizontal();


		// GUILayoutOption은 뭐지?
		if ( GUILayout.Button( "Create!" , GUILayout.Height( 40 ) ) ) {
			GeneralSettings.OpenWindow( GeneralSettings.SettingType.Mage );
		}

		GUILayout.EndArea();
	}

	void DrawWarriorSettings()
	{
		GUILayout.BeginArea( warriorSectionRect );
		GUILayout.Label( "Warrior !" );
		GUILayout.Label( "WarriorClassType !" );
		warriorData.warriorClassType= (Types.WarriorClassType) EditorGUILayout.EnumPopup( warriorData.warriorClassType );

		GUILayout.EndArea();

	}

	void DrawRogueSettings()
	{
		GUILayout.BeginArea( rogueSectionRect );
		GUILayout.Label( "Rogue !" );
		GUILayout.Label( "RogueWpnType !" );
		rogueData.rogueWpnType = (Types.RogueWpnType) EditorGUILayout.EnumPopup( rogueData.rogueWpnType );

		GUILayout.EndArea();
	}
}


public class GeneralSettings : EditorWindow
{
	public enum SettingType
	{
		Mage,
		Warrior,
		Rogue,
	}


	static SettingType dataSetting;
	static GeneralSettings window;


	public static void OpenWindow( SettingType setting )
	{
		dataSetting = setting;
		window = (GeneralSettings) GetWindow( typeof( GeneralSettings ) );
		window.minSize = new Vector2( 250 , 200 );
		window.Show();
	}

	private void OnGUI()
	{
		switch ( dataSetting ) {
			case SettingType.Mage:
				DrawSettings( (CharacterData) EnemyDesignerWindow.MageData );
				break;
			case SettingType.Warrior:
				break;
			case SettingType.Rogue:
				break;
			default:
				break;
		}
	}

	void DrawSettings( CharacterData charData )
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label( "Prefab" );    // : 딱 붙여서 나타난다.
		charData.prefab = (GameObject) EditorGUILayout.ObjectField( charData.prefab , typeof( GameObject ) , false );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label( "Health" );    // : 딱 붙여서 나타난다.
		charData.maxHealth = EditorGUILayout.FloatField( charData.maxHealth );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Energy" ); // : 에디터 클래스 만큼 띄워서 나타난다.
		charData.maxEnergy = EditorGUILayout.DelayedFloatField( charData.maxEnergy );
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Power" ); // : 에디터 클래스 만큼 띄워서 나타난다.
		charData.power = EditorGUILayout.Slider( charData.power , 0 , 100 );
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Cri Chance" ); // : 에디터 클래스 만큼 띄워서 나타난다.
		charData.critChance = EditorGUILayout.Slider( charData.critChance , 0 , 100 );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField( "Name" ); // : 에디터 클래스 만큼 띄워서 나타난다.
		charData.name = EditorGUILayout.TextField( charData.name );
		EditorGUILayout.EndHorizontal();


		if ( charData.prefab == null ) {
			EditorGUILayout.HelpBox( "This enemy needs a [Prefab] before it can be created." , MessageType.Warning );
		}
		else if ( charData.name == null || charData.name.Length < 1 ) {
			EditorGUILayout.HelpBox( "Enemy name is Error" , MessageType.Warning );
		} else if ( GUILayout.Button( "Finish and Save" , GUILayout.Height( 30 ) ) ) {
			SaveCharacterData();
			window.Close();		// 이게 열려져 있는 상태에서 진행이 되면 에러가 날 수 도 있다.
		}
	}

	void SaveCharacterData()
	{
		string prefabPath;
		string newPrefabPath = "Assets/LSH/";
		string dataPath = "Assets/resources/prefab/characterData/data/";

		switch ( dataSetting ) {
			case SettingType.Mage:
				dataPath += "mage/" + EnemyDesignerWindow.MageData.name + ".asset";
				AssetDatabase.CreateAsset( EnemyDesignerWindow.MageData , dataPath );           // 스크립트블 오브젝트를 만든다.

				newPrefabPath += "mage/" + EnemyDesignerWindow.MageData.name + ".prefab";
				prefabPath = AssetDatabase.GetAssetPath( EnemyDesignerWindow.MageData.prefab);  // 프리팹을 만든다.


				AssetDatabase.CopyAsset( prefabPath , newPrefabPath );
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();

				GameObject magePrefab = (GameObject) AssetDatabase.LoadAssetAtPath( newPrefabPath , typeof( GameObject ) );

				if ( !magePrefab.GetComponent<Mage>() ) {
					magePrefab.AddComponent( typeof( Mage ) );
				}

				magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageData;

				break;
			case SettingType.Warrior:
				break;
			case SettingType.Rogue:
				break;
			default:
				break;
		}

	}

}
