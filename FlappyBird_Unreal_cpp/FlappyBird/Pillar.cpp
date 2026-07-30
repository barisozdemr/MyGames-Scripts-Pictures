// Fill out your copyright notice in the Description page of Project Settings.

#include "Pillar.h"

#include "BirdPawn.h"
#include "Components/BoxComponent.h"
#include "BirdGameMode.h"
#include "Kismet/GameplayStatics.h"

APillar::APillar()
{
	PrimaryActorTick.bCanEverTick = true;
	
	BirdGameMode = Cast<ABirdGameMode>(UGameplayStatics::GetGameMode(GetWorld()));
	
	BirdPawn = Cast<ABirdPawn>(UGameplayStatics::GetPlayerPawn(GetWorld(), 0));
	
	RootComponent = CreateDefaultSubobject<USceneComponent>(TEXT("Root"));
	
	ScoreCollider = CreateDefaultSubobject<UBoxComponent>(TEXT("ScoreCollider"));
	ScoreCollider->SetupAttachment(RootComponent);
	
	PillarCollider1 = CreateDefaultSubobject<UBoxComponent>(TEXT("PillarCollider1"));
	PillarCollider1->SetupAttachment(RootComponent);
	
	PillarCollider2 = CreateDefaultSubobject<UBoxComponent>(TEXT("PillarCollider2"));
	PillarCollider2->SetupAttachment(RootComponent);
}

void APillar::BeginPlay()
{
	Super::BeginPlay();
	
	ScoreCollider->OnComponentBeginOverlap.AddDynamic(this, &APillar::OnScoreOverlap);
	
	PillarCollider1->OnComponentHit.AddDynamic(this, &APillar::OnPillarHit);
	PillarCollider2->OnComponentHit.AddDynamic(this, &APillar::OnPillarHit);
}

void APillar::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
	
	AddActorWorldOffset(FVector(0.0f, DeltaTime * -MovementSpeed, 0.0f));
}

void APillar::SetMovementSpeed(float newMovementSpeed)
{
	MovementSpeed = newMovementSpeed;
}

void APillar::OnScoreOverlap(
	UPrimitiveComponent* OverlappedComponent,
	AActor* OtherActor,
	UPrimitiveComponent* OtherComp,
	int32 OtherBodyIndex,
	bool bFromSweep,
	const FHitResult& SweepResult)
{
	BirdGameMode->IncreaseScore();
}

void APillar::OnPillarHit(
	UPrimitiveComponent* HitComponent,
	AActor* OtherActor,
	UPrimitiveComponent* OtherComp,
	FVector NormalImpulse,
	const FHitResult& Hit)
{
	BirdPawn->KillBird();
	BirdGameMode->GameOver();
}