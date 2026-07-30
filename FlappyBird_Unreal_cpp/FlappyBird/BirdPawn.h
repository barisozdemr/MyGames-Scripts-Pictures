// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Pawn.h"
#include "BirdPawn.generated.h"

class UPaperFlipbook;
class UPaperFlipbookComponent;
class UInputMappingContext;
class UCapsuleComponent;
class UInputAction;

UCLASS()
class FLAPPYBIRD_API ABirdPawn : public APawn
{
	GENERATED_BODY()

public:
	ABirdPawn();
	
	virtual void Tick(float DeltaTime) override;
	
	virtual void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent) override;

protected:
	virtual void BeginPlay() override;

private:
	UPROPERTY(VisibleAnywhere)
	UPaperFlipbookComponent* BirdFlipbookComponent;
	
	UPROPERTY(EditAnywhere)
	UPaperFlipbook* BirdDeadFlipbook;
	
	UPROPERTY(VisibleAnywhere)
	UCapsuleComponent* BirdColliderComponent;
	
	UPROPERTY(EditAnywhere, Category="Input_Cpp")
	UInputMappingContext* DefaultMappingContext;
	
	UPROPERTY(EditAnywhere, Category="Input_Cpp")
	UInputAction* JumpAction;
	
	UPROPERTY(EditAnywhere, Category="Input_Cpp")
	UInputAction* SpaceBarAction;
	
	UPROPERTY(EditAnywhere, Category="Jump")
	int32 JumpFactor = 350;
	
	bool IsDead = false;
	
	void OnWPressed();
	void OnSpaceBarPressed();
	
public:	
	void KillBird();
	
	UFUNCTION(BlueprintCallable)
	void StartPhysics();
	
};
