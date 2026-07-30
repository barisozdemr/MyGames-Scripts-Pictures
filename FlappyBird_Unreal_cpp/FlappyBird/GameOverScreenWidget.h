// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "GameOverScreenWidget.generated.h"

class UTextBlock;

UCLASS()
class FLAPPYBIRD_API UGameOverScreenWidget : public UUserWidget
{
	GENERATED_BODY()
	
private:
	UPROPERTY(meta = (BindWidget, AllowPrivateAccess="true"), BlueprintReadOnly)
	UTextBlock* HighestScoreText;
	
public:
	void SetHighestScore(int32 HighestScore);
	
	void OnPlayAgainButtonClicked();
};
