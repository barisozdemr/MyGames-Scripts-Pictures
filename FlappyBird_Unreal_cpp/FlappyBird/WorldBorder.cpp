// Fill out your copyright notice in the Description page of Project Settings.

#include "WorldBorder.h"

#include "BirdPawn.h"
#include "BirdGameMode.h"
#include "Components/BoxComponent.h"
#include "Kismet/GameplayStatics.h"

AWorldBorder::AWorldBorder()
{
	PrimaryActorTick.bCanEverTick = true;
	
	RootComponent = CreateDefaultSubobject<USceneComponent>(TEXT("Root"));
	
	WorldBorderColliderComponent = CreateDefaultSubobject<UBoxComponent>(TEXT("WorldBorder"));
	WorldBorderColliderComponent->SetupAttachment(RootComponent);
}

void AWorldBorder::BeginPlay()
{
	Super::BeginPlay();
	
	WorldBorderColliderComponent->OnComponentBeginOverlap.AddDynamic(this, &AWorldBorder::OnWorldBorderOverlap);
}

void AWorldBorder::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}

void AWorldBorder::OnWorldBorderOverlap(
	UPrimitiveComponent* OverlappedComponent,
	AActor* OtherActor,
	UPrimitiveComponent* OtherComp,
	int32 OtherBodyIndex,
	bool bFromSweep,
	const FHitResult& SweepResult)
{
	if (ABirdPawn* Bird = Cast<ABirdPawn>(OtherActor))
	{
		Bird->KillBird();
	}
	
	if (ABirdGameMode* GameMode = Cast<ABirdGameMode>(UGameplayStatics::GetGameMode(GetWorld())))
	{
		GameMode->GameOver();
	}
}