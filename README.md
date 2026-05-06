# Dungeon of OOP

C# 콘솔 기반 턴제 RPG 팀 프로젝트 예제입니다. Visual Studio에서 `OOP_workspace.sln`을 열고 디버그 실행하면 됩니다.

## 실행 방법

1. Visual Studio 2022 이상 설치
2. `.NET 8 SDK` 워크로드 설치
3. `OOP_workspace.sln` 열기
4. `F5` 또는 `Ctrl+F5` 실행

## 게임 흐름

- 새 게임에서 이름과 직업(전사/마법사/궁수)을 선택합니다.
- 던전에 입장해 슬라임, 고블린, 오크, 마왕을 순서대로 상대합니다.
- 전투 메뉴에서 기본 공격, 직업 스킬, 아이템 사용, 상태 확인을 선택합니다.
- 몬스터 처치 시 경험치와 골드를 획득하고, 모든 몬스터를 처치하면 클리어합니다.

## 과제 요구사항 반영 위치

- 일반 클래스 10개 이상: 캐릭터, 몬스터, 아이템, 시스템 클래스 다수
- 구조체: `Structs/Stat.cs`
- 상속/오버라이딩/base/protected: `Characters` 폴더
- 추상 클래스: `Character`, `Player`, `Monster`, `Item`
- 인터페이스/표준 인터페이스/다중 인터페이스: `Interfaces` 폴더와 `Item`, `Potion`, `Weapon`, `Armor`
- 제네릭/인덱서/컬렉션/람다/out: `Systems/Inventory.cs`, `Systems/InputManager.cs`, `Systems/Game.cs`
- try-catch-finally/throw/사용자 정의 예외: `Systems` 폴더와 `Exceptions` 폴더
- 델리게이트: `Systems/BattleManager.cs`
