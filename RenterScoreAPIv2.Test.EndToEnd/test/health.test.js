import axios from 'axios';
import { expect } from 'chai';

describe('health API testings', () => {
  it('check health', async () => {
    const response = await axios.get('http://localhost:5000/api/v2/health');
    expect(response.status).to.equal(200);
  });
});