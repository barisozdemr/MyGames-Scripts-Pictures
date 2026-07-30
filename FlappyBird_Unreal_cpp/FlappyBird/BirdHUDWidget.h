// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "BirdHUDWidget.generated.h"

class UTextBlock;

UCLASS()
class FLAPPYBIRD_API UBirdHUDWidget : public UUserWidget
{
	GENERATED_BODY()

protected:
	virtual void NativeConstruct() override;

private:
	UPROPERTY(meta = (BindWidget, AllowPrivateAccess="true"), BlueprintReadOnly)
	UTextBlock* ScoreText;
	
	UPROPERTY(meta = (BindWidget, AllowPrivateAccess="true"), BlueprintReadOnly)
	UTextBlock* CountdownText;
	
public:
	void UpdateScore(int32 NewScore);
	
	void UpdateCountdown(int32 Second);
	
	void HideCountdown();
};
