// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Pillar.generated.h"

class ABirdPawn;
class ABirdGameMode;
class UBoxComponent;

UCLASS()
class FLAPPYBIRD_API APillar : public AActor
{
	GENERATED_BODY()
	
public:	
	APillar();
	
	virtual void Tick(float DeltaTime) override;

protected:
	virtual void BeginPlay() override;
	
private:
	UPROPERTY()
	ABirdGameMode* BirdGameMode;
	
	UPROPERTY()
	ABirdPawn* BirdPawn;
	
	UPROPERTY(VisibleAnywhere)
	UBoxComponent* ScoreCollider;
	
	UPROPERTY(VisibleAnywhere)
	UBoxComponent* PillarCollider1;
	UPROPERTY(VisibleAnywhere)
	UBoxComponent* PillarCollider2;
	
	UFUNCTION()
	void OnScoreOverlap(
		UPrimitiveComponent* OverlappedComponent,
		AActor* OtherActor,
		UPrimitiveComponent* OtherComp,
		int32 OtherBodyIndex,
		bool bFromSweep,
		const FHitResult& SweepResult
	);
	
	UFUNCTION()
	void OnPillarHit(
		UPrimitiveComponent* HitComponent,
		AActor* OtherActor,
		UPrimitiveComponent* OtherComp,
		FVector NormalImpulse,
		const FHitResult& Hit
	);

public:	
	void SetMovementSpeed(float MovementSpeed);
	
	int32 MovementSpeed = 150;
};
