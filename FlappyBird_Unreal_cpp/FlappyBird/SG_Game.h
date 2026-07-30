// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/SaveGame.h"
#include "SG_Game.generated.h"

UCLASS()
class FLAPPYBIRD_API USG_Game : public USaveGame
{
	GENERATED_BODY()
	
public:
	UPROPERTY()
	int32 HighestScore = 0;
};
