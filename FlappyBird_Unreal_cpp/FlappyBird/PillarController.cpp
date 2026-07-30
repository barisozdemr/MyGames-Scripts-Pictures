// Fill out your copyright notice in the Description page of Project Settings.

#include "PillarController.h"

#include "Pillar.h"

APillarController::APillarController()
{
	PrimaryActorTick.bCanEverTick = true;
}

void APillarController::BeginPlay()
{
	Super::BeginPlay();
	
	UpdateSpawnTimeDelay();
	
	for (int i = 0; i < 5; i++)
	{
		Pillars.Add(SpawnPillar());
	}
}

void APillarController::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

	SpawnTimer += DeltaTime;
	if (SpawnTimer > SpawnTimeDelay)
	{
		SpawnTimer = 0;
		MovePillar();
		CurrentMoveIndex = (CurrentMoveIndex + 1) % 5;
	}
}

void APillarController::IncreaseMovementSpeed()
{
	MovementSpeed += 30;
}

void APillarController::IncreaseSpawnRate()
{
	SpawnRate += 15;
	
	UpdateSpawnTimeDelay();
	
	SpawnTimer = 0;
}

APillar* APillarController::SpawnPillar()
{
	FVector SpawnLocation = GetActorLocation();

	SpawnLocation.Z += 1000.f;

	FRotator SpawnRotation = FRotator::ZeroRotator;

	APillar* Pillar = GetWorld()->SpawnActor<APillar>(
		PillarClass,
		SpawnLocation,
		SpawnRotation
	);
	
	return Pillar;
}

void APillarController::MovePillar()
{
	FVector SpawnLocation = GetActorLocation();

	SpawnLocation.Z += FMath::FRandRange(-140.f, 140.f); // up and down offset

	FRotator SpawnRotation = FRotator::ZeroRotator;
	
	Pillars[CurrentMoveIndex]->SetActorLocation(SpawnLocation);
	
	Pillars[CurrentMoveIndex]->SetMovementSpeed(MovementSpeed);
}

void APillarController::UpdateSpawnTimeDelay()
{
	SpawnTimeDelay = 3 - (SpawnRate * 0.01f);
	SpawnTimeDelay = FMath::Clamp(SpawnTimeDelay, 0.69, 3);
}