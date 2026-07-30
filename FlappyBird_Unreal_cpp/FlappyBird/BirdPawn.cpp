// Fill out your copyright notice in the Description page of Project Settings.

#include "BirdPawn.h"

#include "PaperFlipbookComponent.h"
#include "Components/CapsuleComponent.h"
#include "EnhancedInputComponent.h"
#include "EnhancedInputSubsystems.h"
#include "InputAction.h"
#include "Kismet/GameplayStatics.h"

ABirdPawn::ABirdPawn()
{
	PrimaryActorTick.bCanEverTick = false;
	
	RootComponent = CreateDefaultSubobject<USceneComponent>(TEXT("Root"));
	
	BirdColliderComponent = CreateDefaultSubobject<UCapsuleComponent>(TEXT("ChickenCollider"));
	BirdColliderComponent->SetupAttachment(RootComponent);
	
	BirdFlipbookComponent = CreateDefaultSubobject<UPaperFlipbookComponent>(TEXT("Flipbook"));
	BirdFlipbookComponent->SetupAttachment(BirdColliderComponent);
}

void ABirdPawn::BeginPlay()
{
	Super::BeginPlay();
	
	if (APlayerController* PC = Cast<APlayerController>(GetController()))
	{
		if (UEnhancedInputLocalPlayerSubsystem* Subsystem =
			ULocalPlayer::GetSubsystem<UEnhancedInputLocalPlayerSubsystem>(PC->GetLocalPlayer()))
		{
			Subsystem->AddMappingContext(DefaultMappingContext, 0);
		}
	}
}

void ABirdPawn::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}

void ABirdPawn::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

	if (UEnhancedInputComponent* EIC = Cast<UEnhancedInputComponent>(PlayerInputComponent))
	{
		EIC->BindAction(
			JumpAction,
			ETriggerEvent::Started,
			this,
			&ABirdPawn::OnWPressed
		);
		
		EIC->BindAction(
			SpaceBarAction,
			ETriggerEvent::Started,
			this,
			&ABirdPawn::OnSpaceBarPressed
		);
	}
}

void ABirdPawn::KillBird()
{
	IsDead = true;
	
	BirdFlipbookComponent->SetFlipbook(BirdDeadFlipbook);
}

void ABirdPawn::StartPhysics()
{
	BirdColliderComponent->SetSimulatePhysics(true);
}

void ABirdPawn::OnWPressed()
{
	if (!IsDead)
	{
		BirdColliderComponent->SetPhysicsLinearVelocity(FVector(0., 0., 0.));
		float Z = static_cast<float>(JumpFactor);
		BirdColliderComponent->AddImpulse(FVector(0., 0., Z));
	}
}

void ABirdPawn::OnSpaceBarPressed()
{
	if (IsDead)
	{
		UGameplayStatics::OpenLevel(
			this,
			FName(*GetWorld()->GetName())
		);
	}
}