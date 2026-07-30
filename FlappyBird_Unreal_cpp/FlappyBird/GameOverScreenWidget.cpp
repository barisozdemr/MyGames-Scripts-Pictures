// Fill out your copyright notice in the Description page of Project Settings.

#include "GameOverScreenWidget.h"

#include "Components/TextBlock.h"
#include "Kismet/GameplayStatics.h"

void UGameOverScreenWidget::SetHighestScore(int32 HighestScore)
{
	if (HighestScoreText)
	{
		HighestScoreText->SetText(FText::AsNumber(HighestScore));
	}
}

void UGameOverScreenWidget::OnPlayAgainButtonClicked()
{
	UGameplayStatics::OpenLevel(
		this,
		FName(*GetWorld()->GetName())
	);
}
