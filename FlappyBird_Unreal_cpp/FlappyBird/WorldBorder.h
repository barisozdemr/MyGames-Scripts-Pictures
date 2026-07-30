// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "WorldBorder.generated.h"

class UBoxComponent;

UCLASS()
class FLAPPYBIRD_API AWorldBorder : public AActor
{
	GENERATED_BODY()
	
public:	
	AWorldBorder();
	
	virtual void Tick(float DeltaTime) override;

protected:
	virtual void BeginPlay() override;

private:
	UPROPERTY(VisibleAnywhere)
	UBoxComponent* WorldBorderColliderComponent;
	
	UFUNCTION()
	void OnWorldBorderOverlap(
		UPrimitiveComponent* OverlappedComponent,
		AActor* OtherActor,
		UPrimitiveComponent* OtherComp,
		int32 OtherBodyIndex,
		bool bFromSweep,
		const FHitResult& SweepResult
	);
};
