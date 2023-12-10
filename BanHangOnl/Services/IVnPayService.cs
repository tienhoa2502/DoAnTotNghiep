﻿using CodeMegaVNPay.Models;
using BanHangOnl.Models;


namespace CodeMegaVNPay.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}