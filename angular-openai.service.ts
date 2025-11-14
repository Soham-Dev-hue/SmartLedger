// openai.service.ts
// Place this in your Angular project: src/app/services/openai.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenAIService {
  private apiUrl = 'https://localhost:7045/api/OpenAI'; // Update with your API URL

  constructor(private http: HttpClient) { }

  // General chat completion
  getChatCompletion(prompt: string, systemMessage?: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/chat`, {
      prompt: prompt,
      systemMessage: systemMessage
    });
  }

  // Analyze invoice data
  analyzeInvoice(invoiceData: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/analyze-invoice`, {
      invoiceData: invoiceData
    });
  }

  // Predict cashflow
  predictCashflow(financialData: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/predict-cashflow`, {
      financialData: financialData
    });
  }
}

// Example Angular component usage:
// 
// export class InvoiceComponent {
//   constructor(private openAIService: OpenAIService) {}
//
//   analyzeInvoice() {
//     const invoiceData = JSON.stringify(this.invoice);
//     this.openAIService.analyzeInvoice(invoiceData).subscribe(
//       response => {
//         console.log('AI Analysis:', response.analysis);
//       },
//       error => {
//         console.error('Error:', error);
//       }
//     );
//   }
// }
