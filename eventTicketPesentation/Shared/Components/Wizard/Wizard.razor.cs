﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace eventTicketPesentation.Shared.Components.Wizard
{
    //Source : https://sinclairinat0r.com/2019/12/08/creating-a-simple-wizard-component-in-blazor
    /// <summary>
    /// Wizard Component
    /// </summary>
    public partial class Wizard
    {
        /// <summary>
        /// List of <see cref="WizardStep"/> added to the Wizard
        /// </summary>
        protected internal List<WizardStep> Steps = new List<WizardStep>();
        

        /// <summary>
        /// The control Id
        /// </summary>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// The ChildContent container for <see cref="WizardStep"/>
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// The Active <see cref="WizardStep"/>
        /// </summary>
        [Parameter]
        public WizardStep ActiveStep { get; set; }

        /// <summary>
        /// The Index number of the <see cref="ActiveStep"/>
        /// </summary>
        [Parameter]
        public int ActiveStepIx { get; set; }

        /// <summary>
        /// Determines whether the Wizard is in the last step
        /// </summary>

        public bool IsLastStep { get; set; }

        /// <summary>
        /// Sets the <see cref="ActiveStep"/> to the previous Index
        /// </summary>

        protected internal void GoBack()
        {
            if (ActiveStepIx > 0)
                SetActive(Steps[ActiveStepIx - 1]);
        }

        /// <summary>
        /// Sets the <see cref="ActiveStep"/> to the next Index
        /// </summary>
        protected internal void GoNext()
        {
            if (ActiveStepIx < Steps.Count - 1)
                SetActive(Steps[(Steps.IndexOf(ActiveStep) + 1)]);
        }

        /// <summary>
        /// Populates the <see cref="ActiveStep"/> the Sets the passed in <see cref="WizardStep"/> instance as the
        /// </summary>
        /// <param name="step">The WizardStep</param>

        protected internal void SetActive(WizardStep step)
        {
            ActiveStep = step ?? throw new ArgumentNullException(nameof(step));

            ActiveStepIx = StepsIndex(step);
            if (ActiveStepIx == Steps.Count - 1)
                IsLastStep = true;
            else
                IsLastStep = false;
        }

        /// <summary>
        /// Retrieves the index of the current <see cref="WizardStep"/> in the Step List
        /// </summary>
        /// <param name="step">The WizardStep</param>
        /// <returns></returns>
        public int StepsIndex(WizardStep step) => StepsIndexInternal(step);
        protected int StepsIndexInternal(WizardStep step)
        {
            if (step == null)
                throw new ArgumentNullException(nameof(step));

            return Steps.IndexOf(step);
        }
        /// <summary>
        /// Adds a <see cref="WizardStep"/> to the WizardSteps list
        /// </summary>
        /// <param name="step"></param>
        protected internal void AddStep(WizardStep step)
        {
            Steps.Add(step);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SetActive(Steps[0]);
                StateHasChanged();
            }
        }

      
    }
}
