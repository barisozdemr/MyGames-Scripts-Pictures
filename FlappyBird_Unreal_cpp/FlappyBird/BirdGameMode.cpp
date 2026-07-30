// Fill out your copyright notice in the Description page of Project Settings.

#include "BirdGameMode.h"

#include "SG_Game.h"
#include "BirdPawn.h"
#include "BirdHUDWidget.h"
#include "GameOverScreenWidget.h"
#include "PillarController.h"
#include "Kismet/GameplayStatics.h"

void ABirdGameMode::BeginPlay()
{
	Super::BeginPlay();
	
	PlayerController = GetWorld()->GetFirstPlayerController();
	
	PlayerController->ConsoleCommand(TEXT("ShowFlag.Tonemapper 0"));
	
	SetInputModeGameOnlyAndHideCursor();
	
	PillarController = Cast<APillarController>(
		UGameplayStatics::GetActorOfClass(GetWorld(), APillarController::StaticClass())
	);
	
	BirdHUD = CreateWidget<UBirdHUDWidget>(GetWorld(), BirdHUDClass);
	if (BirdHUD)
	{
		BirdHUD->AddToViewport();
	}
	
	LoadGame();
	
	StartGame();
}

void ABirdGameMode::StartGame()
{
	Countdown = 3;

	BirdHUD->UpdateCountdown(Countdown);

	GetWorldTimerManager().SetTimer(
		CountdownTimer,
		this,
		&ABirdGameMode::CountdownTick,
		0.7f,
		true
	);
}

void ABirdGameMode::LoadGame()
{
	const FString SlotName = TEXT("default");
	constexpr int32 UserIndex = 0;

	if (UGameplayStatics::DoesSaveGameExist(SlotName, UserIndex))
	{
		SGGameRef = Cast<USG_Game>(
			UGameplayStatics::LoadGameFromSlot(SlotName, UserIndex)
		);
	}
	else
	{
		SGGameRef = Cast<USG_Game>(
			UGameplayStatics::CreateSaveGameObject(USG_Game::StaticClass())
		);
		
		SGGameRef->HighestScore = 0;

		UGameplayStatics::SaveGameToSlot(
			SGGameRef,
			SlotName,
			UserIndex
		);
	}
}

void ABirdGameMode::SaveGame()
{
	const FString SlotName = TEXT("default");
	constexpr int32 UserIndex = 0;
	
	UGameplayStatics::SaveGameToSlot(
		SGGameRef,
		SlotName,
		UserIndex
	);
}

void ABirdGameMode::CountdownTick()
{
	Countdown--;

	if (Countdown > 0)
	{
		BirdHUD->UpdateCountdown(Countdown);
	}
	else
	{
		GetWorldTimerManager().ClearTimer(CountdownTimer);

		BirdHUD->HideCountdown();

		if (ABirdPawn* Bird = Cast<ABirdPawn>(
		UGameplayStatics::GetPlayerPawn(GetWorld(), 0)))
		{
			Bird->StartPhysics();
		}
	}
}

void ABirdGameMode::IncreaseScore()
{
	Score++;
	
	if (Score/5 > ScoreDivision5)
	{
		ScoreDivision5++;
		
		PillarController->IncreaseMovementSpeed();
		PillarController->IncreaseSpawnRate();
	}
	
	BirdHUD->UpdateScore(Score);
}

void ABirdGameMode::GameOver()
{
	CheckForNewHighestScore();
	
	SetGameOverScreen();
	
	SetInputModeGameUIAndShowCursor();
}

void ABirdGameMode::SetGameOverScreen()
{
	GameOverScreen = CreateWidget<UGameOverScreenWidget>(
		GetWorld(),
		GameOverScreenClass
	);
	
	GameOverScreen->SetHighestScore(SGGameRef->HighestScore);
	
	GameOverScreen->AddToViewport();
}

void ABirdGameMode::SetInputModeGameUIAndShowCursor()
{
	PlayerController->bShowMouseCursor = true;
	FInputModeGameAndUI InputMode;
	InputMode.SetWidgetToFocus(GameOverScreen->TakeWidget());
	PlayerController->SetInputMode(InputMode);
}

void ABirdGameMode::SetInputModeGameOnlyAndHideCursor()
{
	PlayerController->bShowMouseCursor = false;
	FInputModeGameOnly InputMode;
	PlayerController->SetInputMode(InputMode);
}

void ABirdGameMode::CheckForNewHighestScore()
{
	if (Score > SGGameRef->HighestScore)
	{
		SGGameRef->HighestScore = Score;
		
		SaveGame();
	}
}