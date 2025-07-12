import { Injectable } from '@angular/core';
import * as forge from 'node-forge';

@Injectable({
  providedIn: 'root'
})
export class KeyGenerationService {

  constructor() {

  }

  generateKeyPair(bits: 1024 | 2048 = 2048) {
    return new Promise<{ privateKeyPem: string, publicKeyPem: string }>((resolve, reject) => {
      forge.pki.rsa.generateKeyPair({ bits, workers: -1 }, (err, keypair) => {
        if (err) {
          reject(err);
        } else {
          const privateKeyPem = forge.pki.privateKeyToPem(keypair.privateKey);
          const publicKeyPem = forge.pki.publicKeyToPem(keypair.publicKey);
          resolve({ privateKeyPem, publicKeyPem });
        }
      });
    });
  }

  signData(privateKeyPem: string, data: string): string {
    const privateKey = forge.pki.privateKeyFromPem(privateKeyPem);
    const md = forge.md.sha256.create();
    md.update(data, 'utf8');
    const signature = privateKey.sign(md);
    return forge.util.encode64(signature);
  }

}
