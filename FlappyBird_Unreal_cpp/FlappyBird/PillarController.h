// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "PillarController.generated.h"

class APillar;

UCLASS()
class FLAPPYBIRD_API APillarController : public AActor
{
	GENERATED_BODY()
	
public:	
	APillarController();
	
	virtual void Tick(float DeltaTime) override;

protected:
	virtual void BeginPlay() override;
	
private:
	UPROPERTY(EditAnywhere, Category="Spawner")
	int32 SpawnRate = 65;
	float SpawnTimeDelay = 3;
	float SpawnTimer = 0.f;

	UPROPERTY(EditAnywhere, Category="Spawner")
	int32 MovementSpeed = 150;

	int32 CurrentMoveIndex = 0;
	
	UPROPERTY()
	TArray<APillar*> Pillars;
	
	APillar* SpawnPillar();
	void MovePillar();
	void UpdateSpawnTimeDelay();
	
public:	
	UFUNCTION(BlueprintCallable)
	void IncreaseMovementSpeed();
	
	UFUNCTION(BlueprintCallable)
	void IncreaseSpawnRate();
	
	UPROPERTY(EditAnywhere, Category = "Spawning")
	TSubclassOf<APillar> PillarClass;
};
