// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "BirdGameMode.generated.h"

class UGameOverScreenWidget;
class USG_Game;
class UUserWidget;
class UBirdHUDWidget;
class APillarController;

UCLASS()
class FLAPPYBIRD_API ABirdGameMode : public AGameModeBase
{
	GENERATED_BODY()
	
protected:
	virtual void BeginPlay() override;
	
private:
	UPROPERTY()
	APlayerController* PlayerController;
	UPROPERTY()
	APillarController* PillarController;
	
	UPROPERTY()
	USG_Game* SGGameRef;
	
	UPROPERTY(VisibleAnywhere, Category="Game")
	int32 Score = 0;
	int32 ScoreDivision5 = 0;
	
	UPROPERTY(EditAnywhere, Category = "UI")
	TSubclassOf<UGameOverScreenWidget> GameOverScreenClass;
	UPROPERTY()
	UGameOverScreenWidget* GameOverScreen;
	
	UPROPERTY(EditAnywhere, Category = "UI")
	TSubclassOf<UBirdHUDWidget> BirdHUDClass;
	UPROPERTY()
	UBirdHUDWidget* BirdHUD;
	
	void SetGameOverScreen();
	
	void CheckForNewHighestScore();
	
	void SetInputModeGameUIAndShowCursor();
	
	void SetInputModeGameOnlyAndHideCursor();
	
	FTimerHandle CountdownTimer;
	int32 Countdown = 3;
	void CountdownTick();
	
	void StartGame();
	
	void LoadGame();
	
	void SaveGame();
	
public:
	void IncreaseScore();
	
	void GameOver();
};
