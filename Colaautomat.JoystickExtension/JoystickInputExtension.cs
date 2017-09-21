﻿using Colaautomat.Core.Models;
using XInput.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colaautomat.Core.Messages;
using Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Colaautomat.JoystickExtension
{
    public class JoystickInputExtension
    {
        private IAutomatInputManager _inputManager;
        private IMaschinenLog _logger;
        private X.Gamepad _gamepad;
        private IProductStorageService _storage;

        public JoystickInputExtension(IEventAggregator eventaggregator, IAutomatInputManager inputManager, IProductStorageService storage, IMaschinenLog log)
        {
            _inputManager = inputManager;
            _logger = log;
            _gamepad = InitDevices();
            _storage = storage;

            // Subscripe to CloseApplicationMessage to stop the joystick polling.
            eventaggregator.GetEvent<CloseApplicationMessage>().Subscribe(Shutdown);
            eventaggregator.GetEvent<ProductDeliveredMessage>().Subscribe(OnProductDeliveredMessage);
        }

        private void OnProductDeliveredMessage(IProduct product)
        {
            if (_gamepad != null)
            {
                _gamepad.FFB_RightMotor(0.5f, 100);
            }
        }

        /// <summary>
        /// Stops polling.
        /// </summary>
        public void Shutdown()
        {
            if (_gamepad != null)
            {
                X.StopPolling();
            }
        }

        private X.Gamepad InitDevices()
        {

            X.Gamepad result = null;
            if (X.IsAvailable)
            {
                result = X.Gamepad_1;
                result.StateChanged += ButtonStateChanged;

                X.StartPolling(result);
            }
            return result;
        }

        private void ButtonStateChanged(object sender, EventArgs e)
        {
            string productName = string.Empty;
            double? coinValue = null;
            bool returnMoneyButton = false;


            if (_gamepad.X_down) productName = "colazero";
            if (_gamepad.Y_down) productName = "fanta";
            if (_gamepad.B_down) productName = "cola";
            if (_gamepad.A_down) returnMoneyButton = true;
            if (_gamepad.Dpad_Down_down) coinValue = 0.2;
            if (_gamepad.Dpad_Up_down) coinValue = 0.5;
            if (_gamepad.Dpad_Right_down) coinValue = 1;

            if (!string.IsNullOrEmpty(productName))
            {
                var product = _storage.getProductByName(productName);
                _inputManager.SelectProduct(product);

            } else
            {
                if (coinValue != null)
                {
                    _inputManager.CoinInput((double)coinValue);
                } else
                {
                    if (returnMoneyButton)
                    {
                        _inputManager.ReturnMoneyButton();
                    }
                }
            } 
        }

        public static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterInstance<JoystickInputExtension>(container.Resolve<JoystickInputExtension>());
        }
    }
}
