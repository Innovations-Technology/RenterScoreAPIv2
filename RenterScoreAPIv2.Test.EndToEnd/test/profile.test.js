import axios from 'axios';
import { expect } from 'chai';

describe('Profile Tab API testing', function () {
  this.timeout(5000);
  
  const testUserId = 43;

  it('get profile tab with valid userId', async () => {
    const response = await axios.get(`http://localhost:5000/api/v2/tab/profile?userId=${testUserId}`);
    expect(response.status).to.equal(200);

    const tab = response.data;
    validateTab(tab);
    validateProfileTab(tab);
  });

  it('get profile tab without userId returns 400', async () => {
    try {
      await axios.get('http://localhost:5000/api/v2/tab/profile');
      throw new Error('Expected error not thrown');
    } catch (error) {
      expect(error.response.status).to.equal(400);
      expect(error.response.data).to.equal('UserId is required for profile tab');
    }
  });

  it('get profile tab with invalid userId format returns error', async () => {
    try {
      await axios.get('http://localhost:5000/api/v2/tab/profile?userId=invalid');
      throw new Error('Expected error not thrown');
    } catch (error) {
      expect(error.response.status).to.be.oneOf([400, 500]);
    }
  });

  it('get profile tab with non-existent userId returns empty resources', async () => {
    const nonExistentUserId = 999999;
    const response = await axios.get(`http://localhost:5000/api/v2/tab/profile?userId=${nonExistentUserId}`);
    expect(response.status).to.equal(200);

    const tab = response.data;
    validateTab(tab);
    
    const profileCard = tab.cards.find(card => card.name === 'profile');
    expect(profileCard).to.exist;
    expect(profileCard.resources).to.be.an('array');
    expect(profileCard.resources).to.be.empty;
  });
});

function validateTab(tab) {
  expect(tab).to.have.property('cards').that.is.an('array');
  expect(tab.cards).to.not.be.empty;
}

function validateProfileTab(tab) {
  const profileCard = tab.cards.find(card => card.name === 'profile');
  expect(profileCard).to.exist;
  expect(profileCard).to.have.property('type').that.equals('card');
  expect(profileCard).to.have.property('resources').that.is.an('array');
  
  if (profileCard.resources.length > 0) {
    const userProfile = profileCard.resources[0];
    validateUserProfile(userProfile);
  }
}

function validateUserProfile(userProfile) {
  expect(userProfile).to.have.property('user_id').that.is.a('number');
  expect(userProfile).to.have.property('email');
  
  // Optional fields can be null
  expect(userProfile).to.have.property('first_name');
  expect(userProfile).to.have.property('last_name');
  expect(userProfile).to.have.property('profile_image');
  expect(userProfile).to.have.property('contact_number');
  expect(userProfile).to.have.property('company');
  expect(userProfile).to.have.property('property_role');
} 