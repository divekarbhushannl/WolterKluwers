---POSTerminalService class is for Single Repository principle
---ICalculatorService,IScan is for OCP principle
---ReceiptGenerate class can implement both IEMAIL and IPrint -Interface segeregation principle
---IPaymentService and IPaymentProcessor is use for Charge the amount either by Card or Cash or Mobile- Dependency Inversion principle

--WolterKluwer.POS.Terminal.API project is for Web API

--WolterKluwerPOSTerminalTest --implemented Test Case using Nunit Framework
Should_return_725M_when_ProductOrder_is_ABCD --return total Amount $7.25 
Should_return_1325M_when_ProductOrder_is_ABCDABA--return total amount $13.25 
Should_return_6M_when_ProductOrder_is_CCCCCCC -return total amount $6.00 
Should_return_null_when_ProductOrder_is_InvalidProductCode -- Return NULL with invaild productcode
