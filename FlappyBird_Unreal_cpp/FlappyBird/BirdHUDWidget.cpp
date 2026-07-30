// Fill out your copyright notice in the Description page of Project Settings.


#include "BirdHUDWidget.h"

#include "Components/TextBlock.h"

void UBirdHUDWidget::NativeConstruct()
{
	Super::NativeConstruct();
}

void UBirdHUDWidget::UpdateScore(int32 NewScore)
{
	if (ScoreText)
	{
		ScoreText->SetText(FText::AsNumber(NewScore));
	}
}

void UBirdHUDWidget::UpdateCountdown(int32 Second)
{
	if (CountdownText)
	{
		CountdownText->SetText(FText::AsNumber(Second));
	}
}

void UBirdHUDWidget::HideCountdown()
{
	if (CountdownText)
	{
		CountdownText->SetVisibility(ESlateVisibility::Hidden);
	}
}
